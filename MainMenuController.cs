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
    public class MainMenuController
    {
        [Menu("main")]
        [Command(path = "personal", name = "Персональная информация")]
        public async void personal(TelegramBotClient Bot, Message message)
        {
            //показатель жизни, количество денег: серебро и золото (отдельно), возраст аккаунта, 
        }

        [Menu("main")]
        [Command(path ="inventory",name ="Инвентарь")]
        public async void inventory(TelegramBotClient Bot, Message message)
        {

        }


        [Menu("main")]
        [Command(path = "cars", name = "Машины")]
        public async void cars(TelegramBotClient Bot, Message message)
        {
            
            await Bot.SendTextMessageAsync(
             message.Chat.Id,
             "Информация о сервере",
             replyMarkup: Utils.reflectMenu("cars"));
        }

        [Menu("main")]
        [Command(path = "flats", name = "Квартиры")]
        public async void flats(TelegramBotClient Bot, Message message)
        {

        }

        [Menu("main")]
        [Command(path = "achievements", name = "Достижения")]
        public async void achievements(TelegramBotClient Bot, Message message)
        {

        }

        [Menu("main")]
        [Command(path = "business", name = "Бизнес")]
        public async void business(TelegramBotClient Bot, Message message)
        {

        }


        [Menu("main")]
        [Command(path = "quests", name = "Задания")]
        public async void quests(TelegramBotClient Bot, Message message)
        {

        }


        [Menu("main")]
        [Command(path = "callback", name = "Написать о проблеме")]
        public async void callback(TelegramBotClient Bot, Message message)
        {

        }

        [Menu("main")]
        [Command(path = "about", name = "О сервере")]
        public async void about(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
              message.Chat.Id,
              "Информация о сервере",
              replyMarkup: Utils.reflectMenu("cars", new AdditionalButton[] {
                 new AdditionalButton("/main","главное меню"),
                 new AdditionalButton("cars:more 5","Больше машин"),
  
              }));

          
        }


        [Menu("second")]
        [Command(path = "detail", name = "Детальнее")]
        public async void detail(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
              message.Chat.Id,
              "Тестовая информация о сервере");
        }

        [Menu("second")]
        [Command(path = "more", name = "Больше инфы")]
        public async void more(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
              message.Chat.Id,
              "Тестовая информация о сервере");
        }



    }
}
