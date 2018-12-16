using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot
{
    [Controller]
    public class MainMenuController
    {
        [Menu("main")]
        [Command(path = "cars", name = "Машины")]
        public async void cars(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var cars =  DB<Cars>
                .all()
                .Skip(0)
                .Take(5);
            List <AdditionalButton> buttons = new List<AdditionalButton>();
            foreach(Cars car in cars)
            {
                buttons.Add(new AdditionalButton($"cars:show_parts_type {car.Id} 0", $"{car.car_title}"));
            }
            buttons.Add(new AdditionalButton("cars:more 1", "Следующий набор"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));

            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               "Информация о машинах",
               replyMarkup: Utils.reflectMenu("cars", buttons.ToArray()));
        }

        [Menu("main")]
        [Command(path = "parts", name = "Автозапчасти")]
        public async void parts(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var parts = DB<Parts>
                .all()
                .Skip(0)
                .Take(5);
            List<AdditionalButton> buttons = new List<AdditionalButton>();
            foreach (Parts part in parts)
            {
                buttons.Add(new AdditionalButton($"parts:show_part {part.Id}", $"{part.title}"));
            }
            buttons.Add(new AdditionalButton("parts:more 1", "Следующий набор"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));

            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               "Информация о запчастях",
               replyMarkup: Utils.reflectMenu("parts", buttons.ToArray()));
        }

        [Menu("main")]
        [Command(path = "basket", name = "Корзина")]
        public async void basket(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var orders = DB<Orders>
                .all()
                .Where(x=>x.telegram_id==message.Chat.Id)
                .Skip(0)
                .Take(5);

            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            StringBuilder buf = new StringBuilder();
            double summary = 0;
            if (orders != null)
            {
                foreach(Orders order in orders)
                {
                    var part = DB<Parts>
                        .all()
                        .Where(x => x.Id == order.parts_id)
                        .First();

                    buf.Append($"{order.Id} *{part.title}* {order.price}\n");
                    summary += order.price;
                    buttons.Add(new InlineKeyboardButton[]
                    {
                          InlineKeyboardButton.WithCallbackData($"Товар {part.title}",$"parts:show_part {part.Id}"),
                          InlineKeyboardButton.WithCallbackData("Убрать",$"basket:remove {order.Id}"),
                    });
                }
                buf.Append($"Всего {summary}");
            }
            else
            {
                buf.Append($"Заказов в корзине нет");
            }

            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               $"Информация о корзине пользователя\n{buf.ToString()}",
               parseMode:ParseMode.Markdown,
               replyMarkup: Utils.reflectAdditionalButtons(
               new AdditionalButton[]
               {
                   new AdditionalButton("basket:buy","Оформить заказ"),
                   new AdditionalButton("/main","Главное меню")
               }    
               ,buttons.ToArray()));
        }

        [Menu("main")]
        [Command(path = "orders", name = "Заказы")]
        public async void orders(TelegramBotClient Bot, Message message)
        {

            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            var orders = DB<Orders>
                .all()
                .Skip(0)
                .Take(5);

            StringBuilder buf = new StringBuilder();
            if (orders != null)
            {
                foreach (Orders order in orders)
                {
                    var part = DB<Parts>
                        .all()
                        .Where(x => x.Id == order.parts_id)
                        .First();

                    buf.Append($"{order.Id} *{part.title}* {order.price}\n");
                  
                    buttons.Add(new InlineKeyboardButton[]
                    {
                          InlineKeyboardButton.WithCallbackData($"Товар {part.title}",$"parts:show_part {part.Id}"),
                          InlineKeyboardButton.WithCallbackData("Убрать",$"basket:remove {order.Id}"),
                    });
                }
            }
            else
            {
                buf.Append($"Заказов в нет");
            }


            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               $"Информация о всех заказах\n{buf.ToString()}",
               parseMode: ParseMode.Markdown,
               replyMarkup: Utils.reflectAdditionalButtons(
               new AdditionalButton[]
               {
                   new AdditionalButton("/main","Главное меню")
               }
               , buttons.ToArray()));
        }

      

    }
}
