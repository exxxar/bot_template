using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Orders
    {
        public virtual int Id { get; protected set; }
        public virtual DateTime date { get; set; }
        public virtual int count { get; set; }
        public virtual double price { get; set; }
        public virtual int parts_id { get; set; }
        public virtual int employees_id { get; set; }
        public virtual int clients_id { get; set; }
        public virtual int telegram_id { get; set; }
       
    }
}
