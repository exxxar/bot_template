using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDriverBot
{
    public static class DB<T>
    {
        
        public static List<T> all()
        {
            var sessionFactory = MySQLSessionFactory.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return (List<T>)session.CreateCriteria(typeof(T))                        
                      .List<T>();

                }
            }

        }
        
        public static void truncate()
        {
            var sessionFactory = MySQLSessionFactory.CreateSessionFactory();
            var metadata = sessionFactory.GetClassMetadata(typeof(T)) 
                as NHibernate.Persister.Entity.AbstractEntityPersister;
            string table = metadata.TableName;

            using (ISession session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    string deleteAll = string.Format("DELETE FROM {0}", table);
                    session.CreateSQLQuery(deleteAll).ExecuteUpdate();

                    transaction.Commit();
                }
            }
        }

        public static object query(string query)
        {
            object result = null;

            var sessionFactory = MySQLSessionFactory.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    result = session.CreateSQLQuery(query).UniqueResult();
                    transaction.Commit();
                }
            }

            return result;
        }

        public static void saveOrUpdate(T table)
        {
            var sessionFactory = MySQLSessionFactory.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(table);
                    transaction.Commit();
   
                }

            }
               
        }


        public static void delete(T table)
        {
            var sessionFactory = MySQLSessionFactory.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(table);
                    transaction.Commit();

                }

            }

        }

    }
}
