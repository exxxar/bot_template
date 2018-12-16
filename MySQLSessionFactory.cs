using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    public static class MySQLSessionFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                MySQLConfiguration.Standard.ConnectionString(c => c              
                    .Server("localhost")              
                    .Database("auto_parts")
                    .Username("root")
                    .Password("")))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))                
                .BuildSessionFactory();
        }
    }
}
