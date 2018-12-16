using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDriverBot.Controllers
{
    [Controller]
    public class StartMenuController
    {

        [Menu("start")]
        [Command(path = "home", name = "Главное меню")]
        public async void main(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             "Сделайте свой выбор",
             replyMarkup: Utils.reflectMenu("main"));
        }

        [Menu("start")]
        [Command(path = "about", name = "Информация о системе")]
        public async void about(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             "Сделайте свой выбор",
             replyMarkup: Utils.reflectMenu("about",new AdditionalButton[] {                 
                    new AdditionalButton("/start", "Назад"),
                    new AdditionalButton("/main", "Главное меню"),
                 }));
        }

       
    }
}
