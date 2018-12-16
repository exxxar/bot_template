using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot.Mappings
{
    public class CarsMap: ClassMap<Entities.Cars>
    {
        public CarsMap()
        {
            Id(x => x.Id);
            Map(x => x.car_title);
            Map(x => x.car_model);
            Map(x => x.year);
        }
    }
}
