using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Parts_stock
    {
        public virtual int Id { get; protected set; }
        public virtual string part_title { get; set; }
        public virtual double price { get; set; }
        public virtual int count { get; set; }
        public virtual int parts_id { get; set; }
        public virtual int shippers_id { get; set; }
    }
}
