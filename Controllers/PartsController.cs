using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot.Controllers
{
    [Controller]
    class PartsController
    {
        [Command(path = "more ([0-9]+)")]
        public async void moreCars(TelegramBotClient Bot, Message message, string page)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<AdditionalButton> buttons = new List<AdditionalButton>();
            var parts = DB<Parts>
                .all()
                .Skip(int.Parse(page) * 5)
                .Take(5);

            if (parts != null)
            {
                foreach (Parts part in parts)
                {
                    buttons.Add(new AdditionalButton($"parts:show_part {part.Id}", $"{part.title}"));
                }

                buttons.Add(new AdditionalButton($"parts:more {int.Parse(page) + 1}", "Следующий набор"));
            }
            if (int.Parse(page) > 0)
                buttons.Add(new AdditionalButton(String.Format("parts:more {0}", int.Parse(page) - 1 > 0 ? int.Parse(page) - 1 : 0), "Предидущий набор"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));


            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             $"Список запчастей для машины, страница {page}",
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [Command(path = "show_part ([0-9]+)")]
        public async void showPart(TelegramBotClient Bot, Message message, string partId)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<AdditionalButton> buttons = new List<AdditionalButton>();
            var part = DB<Parts>
                .all()
                .Where<Parts>(x => x.Id == int.Parse(partId))
                .First();

            StringBuilder info = new StringBuilder();


            if (part != null)
            {
                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                const string file = @"Files/noimage.png";

                var fileName = file.Split(Path.DirectorySeparatorChar).Last();

                using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await Bot.SendPhotoAsync(
                        message.Chat.Id,
                        fileStream,
                        parseMode:ParseMode.Markdown,
                        caption:$"*Запчасть*:{part.title}");
                }

                await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);


                var car = DB<Cars>
                   .all()
                   .Where<Cars>(x => x.Id == part.cars_id)
                   .First();

                var part_type = DB<Parts_type>
                   .all()
                   .Where<Parts_type>(x => x.Id == part.parts_type_id)
                   .First();

                var part_in_stock = DB<Parts_stock>
                   .all()
                   .Where<Parts_stock>(x => x.Id == part.Id)
                   .First();

                info.Append($"*Машина*:{car.car_title} [{car.car_model} {car.year} г.в.]\n");
                info.Append($"*Тип запчасти*:{part_type.title}\n");
                info.Append($"*Наличие на складе*:{part_in_stock.count} [{part_in_stock.price} руб.]\n");
                buttons.Add(new AdditionalButton($"parts:buy {part.Id}", $"В корзину *{part.title}*"));
            
            }
            else
                info.Append("<part not found>");

            buttons.Add(new AdditionalButton("/main", "Главное меню"));


            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             
             $"{info.ToString()}",
             parseMode:ParseMode.Markdown,
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [Command(path = "buy ([0-9]+)")]
        public async void buy(TelegramBotClient Bot, Message message, string partId)
        {
            try
            {
                var part = DB<Parts>
                   .all()
                   .Where<Parts>(x => x.Id == int.Parse(partId))
                   .First();

                var part_in_stock = DB<Parts_stock>
                      .all()
                      .Where<Parts_stock>(x => x.Id == part.Id)
                      .First();

                Clients client = new Clients();
                client.name = message.Contact==null?message.Chat.FirstName: message.Contact.FirstName;
                client.sname = message.Contact == null ? message.Chat.LastName : message.Contact.LastName;
                client.phone = message.Contact == null ? "нет телефона" : message.Contact.PhoneNumber;
                client.tname = message.Contact == null ? $"{message.Chat.Id}":$"{message.Contact.UserId}";
                client.viber = message.Contact == null ? "Нет телефона" : $"{message.Contact.PhoneNumber}";
                client.vk = "";
                client.email = Faker.Internet.Email();
               
                DB<Clients>.saveOrUpdate(client);

                Employees employee = new Employees();
                employee.name = "TEST EMPLOYEE NAME";
                employee.phone = "+38(071)444-44-44";
                employee.post = "TEST POST";
                DB<Employees>.saveOrUpdate(employee);
              
                var clientId = DB<Clients>
                    .all()
                    .Where(x =>x.email == client.email)                       
                    .First().Id;

                var employeeId = DB<Employees>
                    .all()
                    .Where(x =>
                        x.name.Equals(employee.name) &&
                        x.phone.Equals(employee.phone) &&
                        x.post.Equals(employee.post)
                        )
                    .First().Id;

                Orders order = new Orders();
                order.count = 1;
                order.parts_id = int.Parse(partId);
                order.price = part_in_stock.price;
                order.clients_id = clientId;
                order.employees_id = employeeId;
                order.date = DateTime.Now;
                order.telegram_id = (int)message.Chat.Id;
                DB<Orders>.saveOrUpdate(order);
                
             }catch(Exception e) {
                Console.WriteLine(e);
            }

            await Bot.SendTextMessageAsync(
                message.Chat.Id,
            "Спасибо!",
             parseMode: ParseMode.Markdown,
             replyMarkup: Utils.reflectAdditionalButtons(new AdditionalButton[] {
                   new AdditionalButton("basket", "Перейти в корзину"),
                   new AdditionalButton("/main", "Главное меню")
                 }));

          
        }
    }
}
