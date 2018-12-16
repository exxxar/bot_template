using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDriverBot
{
    [Controller]
    class CommandController
    {
       
        [Command(path = @"/keyboard",describe =" send inline keyboard")]
        public async void keyboard(TelegramBotClient Bot,Message message)
        {
            ReplyKeyboardMarkup ReplyKeyboard = new[]
                     {
                        new[] { "1.1", "1.2","test" },
                        new[] { "2.1", "2.2" ,"test","2.3"},
                    };
           
            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Choose",
                replyMarkup: ReplyKeyboard);
        }

        [Command(path = @"/request", describe = " request location or contact")]
        public async void request(TelegramBotClient Bot, Message message)
        {
            var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestLocation("Location"),
                        KeyboardButton.WithRequestContact("Contact"),
                    });

            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Who or Where are you?",
                replyMarkup: RequestReplyKeyboard);
     
        }


        [Command(path = @"/start", describe = " стартовое меню")]
        public async void start(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

            const string file = @"Files/logo.png";

            var fileName = file.Split(Path.DirectorySeparatorChar).Last();

            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await Bot.SendPhotoAsync(
                    message.Chat.Id,
                    fileStream,
                    "Магазин автозапчастей");
            }

            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Сделайте свой выбор",
                replyMarkup: Utils.reflectMenu("start", new AdditionalButton[] {
                    new AdditionalButton("http://vk.com","Мы в VK")
                 }));

        }


        [Command(path = @"/main", describe = " главное меню")]
        public async void main(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            
            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Сделайте свой выбор",
                replyMarkup:Utils.reflectMenu("main"));

            //,null,new InlineKeyboardButton[][] {
            //        new InlineKeyboardButton[] {
            //            InlineKeyboardButton.WithCallbackData("test 1","test 1"),
            //            InlineKeyboardButton.WithCallbackData("test 2","test 2"),
            //            InlineKeyboardButton.WithCallbackData("test 3","test 3")
            //        },
            //       new InlineKeyboardButton[] {
            //            InlineKeyboardButton.WithCallbackData("test 4","test 4"),
            //            InlineKeyboardButton.WithCallbackData("test 5","test 5"),
            //            InlineKeyboardButton.WithCallbackData("test 6","test 6")
            //        },
            //       new InlineKeyboardButton[] {

            //            InlineKeyboardButton.WithUrl("VK","http://vk.com"),

            //        },
            //    }

        }


        [Command(path = @"/photo", describe = " send a photo")]
        public async void photo(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

            const string file = @"Files/tux.png";

            var fileName = file.Split(Path.DirectorySeparatorChar).Last();

            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await Bot.SendPhotoAsync(
                    message.Chat.Id,
                    fileStream,
                    "Nice Picture");
            }


        }

       

    }
}
