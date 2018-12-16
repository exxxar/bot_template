using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Shippers
    {
        public virtual int Id { get; protected set; }
        public virtual string title { get; set; }
        public virtual string adress { get; set; }
        public virtual string phone { get; set; }
        public virtual string city { get; set; }

    }
}
