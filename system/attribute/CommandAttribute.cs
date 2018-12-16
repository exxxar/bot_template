using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    [AttributeUsage(AttributeTargets.Method)]
    class CommandAttribute : System.Attribute
    {
        
        public string path { get; set; }
        public string name { get; set; }
        public string describe { get; set; }
        public CommandAttribute()
        {
            this.path = "";
            this.name = "";
            this.describe = null;
        }

      
        public CommandAttribute(string path, string describe = null, string name="")
        {
            this.path = path;
            this.name = name;
            this.describe = describe;
        }


    }
}
