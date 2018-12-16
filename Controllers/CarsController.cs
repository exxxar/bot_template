using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot
{
    [Controller]
    class CarsController
    {
       
        [Command(path = "more ([0-9]+)")]
        public async void moreCars(TelegramBotClient Bot, Message message,string page)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<AdditionalButton> buttons = new List<AdditionalButton>();
            var cars = DB<Cars>
                .all()
                .Skip(int.Parse(page) * 5)
                .Take(5);

            if (cars != null)
            {
                foreach (Cars car in cars)
                {
                    buttons.Add(new AdditionalButton($"cars:show_parts_type {car.Id} 0", $"{car.car_title} [{car.year}]"));
                }

                buttons.Add(new AdditionalButton($"cars:more {int.Parse(page) + 1}", "Следующий набор"));
            }
            if (int.Parse(page)>0)
                buttons.Add(new AdditionalButton(String.Format("cars:more {0}", int.Parse(page) - 1 > 0 ? int.Parse(page) - 1 : 0), "Предидущий набор"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));


            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             $"Список машин, страница {page}",
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [Command(path = "show_parts_type ([0-9]+) ([0-9]+)")]
        public async void showPartsType(TelegramBotClient Bot, Message message, string carId,string page)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<AdditionalButton> buttons = new List<AdditionalButton>();
            var partsType = DB<Parts_type>
                .all()
                .Skip(int.Parse(page) * 5)
                .Take(5);

            if (partsType != null)
            {
             
                foreach (Parts_type type in partsType)
                {
                    Console.WriteLine(type.title);
                    buttons.Add(new AdditionalButton($"cars:show_parts_by_type {type.Id} {carId} 0", $"{type.title}"));
                }
               
                buttons.Add(new AdditionalButton($"cars:show_parts_type {carId} {int.Parse(page)+1}", "Следующий набор"));
            }

            if (int.Parse(page) > 0)
                buttons.Add(new AdditionalButton(String.Format("cars:show_parts_type {0} {1}",carId, int.Parse(page) - 1 > 0 ? int.Parse(page) - 1 : 0), "Предидущий набор"));

            buttons.Add(new AdditionalButton("/main", "Главное меню"));

            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             $"Тип запчасти, страница *{page}*",
             parseMode:ParseMode.Markdown,
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [Command(path = "show_parts_by_type ([0-9]+) ([0-9]+) ([0-9]+)")]
        public async void showPartsByType(TelegramBotClient Bot, Message message, string typeId, string carId, string page)
        {
            var partsByTypeAndCar = DB<Parts>
                .all()
                .Where(x => x.cars_id==int.Parse(carId)&&x.parts_type_id==int.Parse(typeId))
                .Skip(int.Parse(page) * 5)
                .Take(5);

            var partsType = DB<Parts_type>
                .all()
                .Where(x => x.Id == int.Parse(typeId))
                .First();

            var car = DB<Cars>
                .all()
                .Where(x => x.Id == int.Parse(carId))
                .First();

            List<AdditionalButton> buttons = new List<AdditionalButton>();

            if (partsByTypeAndCar != null)
            {

                foreach (var parts in partsByTypeAndCar)
                {
                  
                    buttons.Add(new AdditionalButton($"parts:buy {parts.Id}", $"В корзину *{parts.title}*"));
                }

                buttons.Add(new AdditionalButton($"cars:show_parts_by_type {typeId} {carId} {int.Parse(page) + 1}", "Следующий набор"));
            }

            if (int.Parse(page) > 0)
                buttons.Add(new AdditionalButton(String.Format("cars:show_parts_by_type {0} {1} {2}", typeId ,carId, int.Parse(page) - 1 > 0 ? int.Parse(page) - 1 : 0), "Предидущий набор"));

            buttons.Add(new AdditionalButton("/main", "Главное меню"));
            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             $"Информация о запчасти\n*{partsType.title}* для автомобиля _{car.car_title}_ _{car.car_model}_,\nстраница *{page}*",
             parseMode: ParseMode.Markdown,
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        

    }
}
