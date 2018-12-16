using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDriverBot
{
    public class AdditionalButtonsGroup
    {
        private List<InlineKeyboardButton[]> list { get; set; }

        public AdditionalButtonsGroup(AdditionalButton[] buttons)
        {
            this.list = new List<InlineKeyboardButton[]>();

            List<InlineKeyboardButton> tmp = new List<InlineKeyboardButton>();
            foreach (var button in buttons)
                tmp.Add(InlineKeyboardButton.WithCallbackData(button.name, button.command));
            this.list.Add(tmp.ToArray()); 
        }
        public List<InlineKeyboardButton[]> toList()
        {
            return this.list;
        }

        public InlineKeyboardButton[][] toArray()
        {
            return this.list.ToArray();
        }
    }
}
