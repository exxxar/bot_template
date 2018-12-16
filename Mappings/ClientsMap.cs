using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class ClientsMap: ClassMap<Entities.Clients>
    {
        
        public ClientsMap()
        {
            Id(x => x.Id);
            Map(x => x.name);
            Map(x => x.sname);
            Map(x => x.tname);
            Map(x => x.phone);
            Map(x => x.viber);
            Map(x => x.vk);
            Map(x => x.email);
        }
        
    }
}
