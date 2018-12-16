using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot.Controllers
{
    [Controller]
    public class BasketController
    {
        [Command(path = "buy")]
        public async void buy(TelegramBotClient Bot, Message message)
        {
            var orders = DB<Orders>
              .all()
              .Where(x => x.telegram_id == message.Chat.Id)
              .Skip(0)
              .Take(5);

            List<LabeledPrice> buttons = new List<LabeledPrice>();
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

                    buf.Append($"{order.Id} {part.title} {order.price}\n");
                    summary += order.price;

                    buttons.Add(new LabeledPrice($"{part.title}", ((int)order.price * 100) + 50));

                }
                buf.Append($"Всего {summary}");
            }
            else
            {
                buf.Append($"Заказов в корзине нет");
            }

            if (buttons.Count>0)
                await Bot.SendInvoiceAsync(
                    (int)message.Chat.Id,
                    $"Покупка автозапчастей ",
                    $"{buf.ToString()}",
                    "1",
                    "632593626:TEST:i56982357197",
                    "2",
                    "RUB",
                    buttons.ToArray(),
                    photoWidth: 300,
                    photoHeight: 300,
                    needEmail: true,
                    needName: true,
                    needPhoneNumber: true,
                    needShippingAddress: true,

                    photoUrl: "http://perfumerylab.ru/wp-content/uploads/oplatazakaza.jpg"

                    );
            else
                await Bot.SendTextMessageAsync(
                         message.Chat.Id,
                         $"Нечего покупать, заказов так-то нет",
                         replyMarkup: Utils.reflectAdditionalButtons(new AdditionalButton[] {
                                new AdditionalButton("/main", "Главное меню"),
                             }));

        }

        [Command(path = "remove ([0-9]+)")]
        public async void remove(TelegramBotClient Bot, Message message, string orderId)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var removedOrder = DB<Orders>
                 .all()
                 .Where(x => x.Id == int.Parse(orderId))
                 .First();
            DB<Orders>.delete(removedOrder);

            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var orders = DB<Orders>
                .all()
                .Where(x => x.telegram_id == message.Chat.Id)
                .Skip(0)
                .Take(5);

            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
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
               parseMode: ParseMode.Markdown,
               replyMarkup: Utils.reflectAdditionalButtons(
               new AdditionalButton[]
               {
                   new AdditionalButton("basket:buy","Оформить заказ"),
                   new AdditionalButton("/main","Главное меню")
               }
               , buttons.ToArray()));
        }

    }
}
