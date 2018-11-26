using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDriverBot
{
    [Controller]
    class CarsController
    {
        [Menu("cars")]
        [Command(path = "all", name = "Все машины")]
        public async void more(TelegramBotClient Bot, Message message)
        {
            Console.WriteLine("test 2");
            await Bot.SendTextMessageAsync(
              message.Chat.Id,
              "Тестовая информация о сервере");
        }

        [Menu("cars")]
        [Command(path = "my", name = "Мои машины")]
        public async void more2(TelegramBotClient Bot, Message message)
        {
            Console.WriteLine("test 1");
            await Bot.SendTextMessageAsync(
              message.Chat.Id,
              "Тестовая информация о сервере");
        }

       
        [Command(path = "more ([0-9]+)")]
        public async void moreCars(TelegramBotClient Bot, Message message,string page)
        {
            
            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             "Информация о сервере",
             replyMarkup: Utils.reflectAdditionalButtons(new AdditionalButton[] {
                 new AdditionalButton("/main","главное меню"),
                 new AdditionalButton(String.Format("cars:more {0}",int.Parse(page)-5>0?int.Parse(page)-5:0),"Предидущий набор"),
                 new AdditionalButton(String.Format("cars:more {0}",int.Parse(page)+5),"Следующий набор")

             }));
        }
    }
}
