﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Entities
{
    public class Clients
    {
       
        public virtual int Id { get; protected set; }
        public virtual string name { get; set; }
        public virtual string sname { get; set; }
        public virtual string tname { get; set; }
        public virtual string vk { get; set; }
        public virtual string viber { get; set; }
        public virtual string phone { get; set; }
        public virtual string email { get; set; }
        //public virtual string telegram { get; set; }
    }
}
