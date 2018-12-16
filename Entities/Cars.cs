using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Cars
    {
        public virtual int Id { get; protected set; }
        public virtual string car_model { get; set; }
        public virtual string car_title { get; set; }
        public virtual DateTime year { get; set; }
    }
}
