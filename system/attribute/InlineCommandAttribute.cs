using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    [AttributeUsage(AttributeTargets.Method)]
    class InlineCommandAttribute : System.Attribute
    {
        public string path { get; set; }

        public InlineCommandAttribute()
        {
            this.path = "";
        }


        public InlineCommandAttribute(string path)
        {
            this.path = path;
        }
    }
}
