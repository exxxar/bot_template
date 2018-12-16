using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot.Controllers
{
    [Controller]
    public class EmployeesController
    {
        [Command(path = "more ([0-9]+)")]
        public async void developers(TelegramBotClient Bot, Message message,String page)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            List<AdditionalButton> buttons = new List<AdditionalButton>();
            var employees = DB<Employees>
                .all()
                .Skip(int.Parse(page) * 5)
                .Take(5);

            StringBuilder buf = new StringBuilder();
            if (employees != null)
            {
                foreach (Employees employee in employees)
                {
                    buf.Append($"{employee.Id} {employee.name} {employee.post} {employee.phone}\n");
                }

                buttons.Add(new AdditionalButton($"employees:more {int.Parse(page) + 1}", "Следующий набор"));
            }
            if (int.Parse(page) > 0)
                buttons.Add(new AdditionalButton(String.Format("employees:more {0}", int.Parse(page) - 1 > 0 ? int.Parse(page) - 1 : 0), "Предидущий набор"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));


            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             $"{buf.ToString()}",
             replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }
    }
}
