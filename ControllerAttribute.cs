using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerAttribute : System.Attribute
    {
        public string prefix { get; set; }

        public ControllerAttribute()
        {
            this.prefix = null;
        }
    }
}
