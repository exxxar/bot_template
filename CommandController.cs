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


        [Command(path = @"/main", describe = " send inline keyboard")]
        public async void inline(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            
            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Сделайте свой выбор",
                replyMarkup:Utils.reflectMenu("main"));

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

        [Menu("main")]
        [Command(path = @"/test")]
        public async void test(TelegramBotClient Bot, Message message)
        {
           
        }



    }
}
