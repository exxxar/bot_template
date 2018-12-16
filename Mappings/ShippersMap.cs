using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class ShippersMap : ClassMap<Entities.Shippers>
    {
        public ShippersMap()
        {
            Id(x => x.Id);
            Map(x => x.title);
            Map(x => x.adress);
            Map(x => x.city);
            Map(x => x.phone);
        }
    }
}
