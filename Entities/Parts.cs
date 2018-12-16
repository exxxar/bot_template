using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Parts
    {
        public virtual int Id { get; protected set; }
        public virtual string title { get; set; }
        public virtual int cars_id { get; set; }
        public virtual int parts_type_id { get; set; }
    }
}
