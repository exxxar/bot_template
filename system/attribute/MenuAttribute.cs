using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuAttribute : System.Attribute
    {

        public string name { get; set; }

        public MenuAttribute(string name)
        {
            this.name = name;
        }
     
    }
}
