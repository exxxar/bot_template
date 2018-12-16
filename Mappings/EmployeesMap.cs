using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    public class EmployeesMap : ClassMap<Entities.Employees>
    {
        public EmployeesMap()
        {
            Id(x => x.Id);
            Map(x => x.name);
            Map(x => x.phone);
            Map(x => x.post);
        }
            
    }
}
