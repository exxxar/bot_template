using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class OrdersMap : ClassMap<Entities.Orders>
    {
        public OrdersMap()
        {
            Id(x => x.Id);
            Map(x => x.clients_id);
            Map(x => x.count);
            Map(x => x.date);
            Map(x => x.employees_id);
            Map(x => x.parts_id);
            Map(x => x.telegram_id);
        }
    
    }
}
