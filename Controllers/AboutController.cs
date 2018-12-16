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

namespace TelegramDriverBot
{
    [Controller]
    public class AboutController
    {
        [Menu("about")]
        [Command(path = "faq", name = "F.A.Q.")]
        public async void faq(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadDocument);

            const string file = @"Files/help.docx";

            var fileName = file.Split(Path.DirectorySeparatorChar).Last();

            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await Bot.SendDocumentAsync(
                    message.Chat.Id,
                    fileStream,
                    "Помощь в использовании сервиса");
            }

            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            await Bot.SendTextMessageAsync(
                message.Chat.Id,
                "Сделайте свой выбор",
                replyMarkup: Utils.reflectAdditionalButtons(new AdditionalButton[] {
                    new AdditionalButton("http://vk.com","Описание по использованию в ВК"),
                    new AdditionalButton("/main", "Главное меню")
                 }));
        }

        [Menu("about")]
        [Command(path = "developers", name = "Разработчики")]
        public async void developers(TelegramBotClient Bot, Message message)
        {
            await Bot.SendTextMessageAsync(
             message.Chat.Id,             
             "Информация о разработчиках: *Шейко Константин*",
             parseMode: ParseMode.Markdown
             );
        }

        [Menu("about")]
        [Command(path = "employees", name = "Сотрудники")]
        public async void online(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var employees = DB<Employees>
                .all()
                .Skip(0)
                .Take(5);
            List<AdditionalButton> buttons = new List<AdditionalButton>();
            StringBuilder buf = new StringBuilder();
            foreach (Employees employee in employees)
            {
                buf.Append($"{employee.Id} {employee.name} {employee.post} {employee.phone}\n");
            }
            buttons.Add(new AdditionalButton("employees:more 1", "Следующие"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));

            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               $"{buf.ToString()}",
               replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [Menu("about")]
        [Command(path = "clients", name = "Клиенты")]
        public async void info(TelegramBotClient Bot, Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            var clients = DB<Clients>
                .all()
                .Skip(0)
                .Take(5);
            List<AdditionalButton> buttons = new List<AdditionalButton>();
            StringBuilder buf = new StringBuilder();
            foreach (Clients client in clients)
            {
                buf.Append($"{client.Id} {client.name} {client.tname} {client.phone}\n");
            }
            buttons.Add(new AdditionalButton("clients:more 1", "Следующие"));
            buttons.Add(new AdditionalButton("/main", "Главное меню"));

            await Bot.SendTextMessageAsync(
               message.Chat.Id,
               $"{buf.ToString()}",
               replyMarkup: Utils.reflectAdditionalButtons(buttons.ToArray()));
        }

        [InlineCommand(path = "parts")]
        public async void about(TelegramBotClient Bot, InlineQuery inlineQuery)
        {

            InlineQueryResultBase[] results = {
                new InlineQueryResultArticle(
                    id:"1",
                    title:"test",
                    inputMessageContent: new InputTextMessageContent("HEEEEELP")
                    )
                {
                    HideUrl = false,
                    ThumbHeight = 300,
                    ThumbWidth = 300,
                    ThumbUrl = "https://core.telegram.org/file/811140680/2/P3E5RVFzGZ8/5ae6f9c9610b0cbace",
                    Description = "TEST DESCRIPTION",
                    Url = "http://vk.com/exxxar",
            
                },
                new InlineQueryResultLocation(
                    id: "2",
                    latitude: 40.7058316f,
                    longitude: -74.2581888f,
                    title: "New York")   // displayed result
                    {
                    Title = "test",
                        InputMessageContent = new InputLocationMessageContent(
                            latitude: 40.7058316f,
                            longitude: -74.2581888f)    // message if result is selected
                            
                    },

                new InlineQueryResultLocation(
                    id: "3",
                    latitude: 13.1449577f,
                    longitude: 52.507629f,
                    title: "Berlin") // displayed result
                    {

                        InputMessageContent = new InputLocationMessageContent(
                            latitude: 13.1449577f,
                            longitude: 52.507629f)   // message if result is selected
                    }
 };

            await Bot.AnswerInlineQueryAsync(
               inlineQuery.Id,
               results,
               isPersonal: true,
               cacheTime: 0);
        }

       


    }
}
