using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class Parts_stockMap : ClassMap<Entities.Parts_stock>
    {
        public Parts_stockMap()
        {
            Table("parts_stock");
            Id(x => x.Id);
            Map(x => x.count);
            Map(x => x.parts_id);
            Map(x => x.part_title);
            Map(x => x.price);
            Map(x => x.shippers_id);
        }
    }
}
