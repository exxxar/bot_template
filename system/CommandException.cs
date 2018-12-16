using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    class CommandException:Exception
    {
        public int code { get; set; }
        public CommandException(String message, int code):base(message)
        {
            this.code = code;
        }
    }
}
