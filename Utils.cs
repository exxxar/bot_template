using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDriverBot
{
    public static class Utils
    {
        
        private static TelegramBotClient Bot;

        public static TelegramBotClient getBotInstance()
        {
            if (Bot != null)
                return Bot;
            return new TelegramBotClient("600598222:AAEj-D5CEpPru4sfWstq4KUClp0rjp53lmg");

        }

        public static void reflectCommands(Message message, string data = null)
        {

            Boolean isFinde = false;
            int errorCode = 0;
            string dataCommand = (data == null ? message.Text : data);
            string[] splittedCommand = null;
            if (dataCommand.IndexOf(":") != -1)
                splittedCommand = dataCommand.Split(':');

            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();

            StringBuilder usage = new StringBuilder();

            foreach (Type type in typelist)
            {
                if (type.GetCustomAttribute(typeof(ControllerAttribute)) != null)
                {
                    MethodInfo[] attrs = type.GetMethods();
                    
                    ControllerAttribute ca = (ControllerAttribute)Attribute.GetCustomAttribute(type, typeof(ControllerAttribute));

                    if (splittedCommand != null)
                    {
                        
                        if (ca.prefix == null && !type.GetTypeInfo().Name.ToLower().Replace("controller", "").Trim().Equals(splittedCommand[0].Trim()))
                            continue;
                
                        if (ca.prefix!=null)
                            if (!ca.prefix.Equals(splittedCommand[0])&& !type.GetTypeInfo().Name.ToLower().Replace("controller", "").Equals(splittedCommand[0]))
                                continue;

                    }
                    
                    foreach (MethodInfo m in attrs)
                    {
                        MenuAttribute menu = (MenuAttribute)Attribute.GetCustomAttribute(m, typeof(MenuAttribute));
                        CommandAttribute tx = (CommandAttribute)Attribute.GetCustomAttribute(m, typeof(CommandAttribute));
                        MatchCollection mc = null;

                        if (tx != null)
                        {
                            Regex reg = new Regex(tx.path);
                            mc = reg.Matches(splittedCommand == null ? dataCommand : splittedCommand[1]);
                        }

                        foreach (CustomAttributeData cd in m.CustomAttributes)
                        {

                           

                            if (cd.AttributeType == typeof(CommandAttribute) && tx != null && menu == null)
                                usage.AppendFormat("{0} - {1}\n", tx.path, tx.describe == null ? "<no description>" : tx.describe);


                            if (cd.AttributeType == typeof(CommandAttribute)
                                && mc.Count != 0)
                            {
                                try
                                {
                                    List<object> obj = new List<object>();
                                    obj.Add(getBotInstance());
                                    obj.Add(message);
                                    foreach (Match item in mc)
                                    {
                                        for (int i = 1; i < item.Groups.Count; i++)
                                        {
                                            obj.Add(item.Groups[i].Value);
                                        }
                                    }
                                    isFinde = true;
                                    m.Invoke(Activator.CreateInstance(type), obj.ToArray());
                                }
                                catch (TargetInvocationException e)
                                {
                                    errorCode = ((CommandException)e.InnerException).code;
                                    break;
                                }
                            }

                        }

                    }
                }

            }

            if (!isFinde)
            {
                getBotInstance().SendTextMessageAsync(
                message.Chat.Id,
                usage.ToString(),
                replyMarkup: new ReplyKeyboardRemove());
            }
        }



        public static InlineKeyboardMarkup reflectMenu(string name="main",AdditionalButton[] buttons = null)
        {
            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();

            List<InlineKeyboardButton[]> inlineKeyboard = new List<InlineKeyboardButton[]>();


            foreach (Type type in typelist)
            {
                if (type.GetCustomAttribute(typeof(ControllerAttribute)) != null)
                {
                    MethodInfo[] attrs = type.GetMethods();

                    ControllerAttribute ca = (ControllerAttribute)Attribute.GetCustomAttribute(type, typeof(ControllerAttribute));

                    
                    foreach (MethodInfo m in attrs)
                    {

                        MenuAttribute menu = (MenuAttribute)Attribute.GetCustomAttribute(m, typeof(MenuAttribute));
                        CommandAttribute command = (CommandAttribute)Attribute.GetCustomAttribute(m, typeof(CommandAttribute));
                        if (menu != null && command != null&& menu.name.Trim().Equals(name))
                        {
                            inlineKeyboard.Add(new[] {
                                   InlineKeyboardButton.WithCallbackData(command.name,
                                   ca.prefix==null?
                                        String.Format("{0}:{1}",
                                            type.GetTypeInfo().Name.ToLower().Replace("controller", ""),
                                             command.path):String.Format("{0}:{1}",ca.prefix,command.path))

                                });
                            Console.WriteLine("{0} {1} ", command.name,
                                  ca.prefix == null ?
                                        String.Format("{0}:{1}",
                                            type.GetTypeInfo().Name.ToLower().Replace("controller", ""),
                                             command.path) : String.Format("{0}:{1}", ca.prefix, command.path));
                        }
                    }
                }

            }
           
            if (buttons != null)
            {
                foreach (var item in reflectButtons(buttons))
                    inlineKeyboard.Add(item);
            }
            return new InlineKeyboardMarkup(inlineKeyboard.ToArray());
        }

        public static InlineKeyboardMarkup reflectAdditionalButtons(AdditionalButton[] buttons)
        {
            return new InlineKeyboardMarkup(reflectButtons(buttons).ToArray());
        }

        private static List<InlineKeyboardButton[]> reflectButtons(AdditionalButton[] buttons)
        {
            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();
            List<InlineKeyboardButton[]> inlineKeyboard = new List<InlineKeyboardButton[]>();

            foreach (AdditionalButton item in buttons)
            {
                Boolean isExists = false;
                string[] splittedCommand = null;
                if (item.command.IndexOf(":") != -1)
                    splittedCommand = item.command.Split(':');

                foreach (Type type in typelist)
                {
                    if (type.GetCustomAttribute(typeof(ControllerAttribute)) != null)
                    {
                        MethodInfo[] attrs = type.GetMethods();

                        ControllerAttribute ca = (ControllerAttribute)Attribute.GetCustomAttribute(type, typeof(ControllerAttribute));

                        if (splittedCommand != null)
                        {

                            if (ca.prefix == null && !type.GetTypeInfo().Name.ToLower().Replace("controller", "").Trim().Equals(splittedCommand[0].Trim()))
                                continue;

                            if (ca.prefix != null)
                                if (!ca.prefix.Equals(splittedCommand[0]) && !type.GetTypeInfo().Name.ToLower().Replace("controller", "").Equals(splittedCommand[0]))
                                    continue;

                        }

                        foreach (MethodInfo m in attrs)
                        {

                            CommandAttribute command = (CommandAttribute)Attribute.GetCustomAttribute(m, typeof(CommandAttribute));
                            if (command == null)
                                continue;

                            MatchCollection mc = null;

                            Regex reg = new Regex(command.path);
                            mc = reg.Matches(splittedCommand == null ? item.command : splittedCommand[1]);

                            if (mc.Count != 0)
                            {

                                isExists = true;
                                inlineKeyboard.Add(new[] {
                                        InlineKeyboardButton.WithCallbackData(item.name,splittedCommand == null ? item.command : String.Join(":",splittedCommand))
                                    });

                            }
                        }


                    }

                }

                if (!isExists)
                    inlineKeyboard.Add(new[] {
                                   InlineKeyboardButton.WithCallbackData(String.Format("Неопознанная команда {0}",item.command),item.command ) });

            }


            return inlineKeyboard;
        }
    }
}
