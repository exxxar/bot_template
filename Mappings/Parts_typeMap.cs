using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class Parts_typeMap : ClassMap<Entities.Parts_type>
    {
        public Parts_typeMap()
        {
            Table("parts_type");
            Id(x => x.Id);
            Map(x => x.title);
        }
    }
}
