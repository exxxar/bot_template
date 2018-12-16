using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Employees
    {
        public virtual int Id { get; protected set; }
        public virtual string name { get; set; }
        public virtual string post { get; set; }
        public virtual string phone { get; set; }
    }
}
