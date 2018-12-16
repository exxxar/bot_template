using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class PartsMap : ClassMap<Entities.Parts>
    {
        public PartsMap()
        {
            Id(x => x.Id);
            Map(x => x.cars_id);
            Map(x => x.parts_type_id);
            Map(x => x.title);
        }
    }
}
