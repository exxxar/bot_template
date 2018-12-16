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

namespace TelegramDriverBot.Controllers
{
    [Controller]
    public class OrdersContoller
    {
        [Command(path = "more ([0-9]+)")]
        public async void more(TelegramBotClient Bot, Message message, String page)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            var orders = DB<Orders>
                .all()
                .Skip(int.Parse(page) * 5)
                .Take(5);

            StringBuilder buf = new StringBuilder();
            double summary = 0;
            if (orders != null)
            {
                foreach (Orders order in orders)
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
                          InlineKeyboardButton.WithCallbackData("Убрать",$"orders:remove {order.Id}"),
                    });
                }
                buf.Append($"Всего {summary}");
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
