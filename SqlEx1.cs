using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Cache.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class Person123
    {
        [QuerySqlField]
        public string Name { get; set; }

        [QuerySqlField]
        public int Age { get; set; }
}
    class SqlEx1
    {
        static void Main(string[] args)
        {
            IIgnite ignite = Ignition.Start();
            ICache<int, Person123> cache = ignite.GetOrCreateCache<int, Person123>(new CacheConfiguration("persons", typeof(Person123)));
            var sqlQuery = new SqlQuery(typeof(Person123), "where age < ?", 10);

            IQueryCursor<ICacheEntry<int, Person123>> queryCursor = cache.Query(sqlQuery);


            /*
                var fieldsQuery = new SqlFieldsQuery("select name from Person where age > ?", 30);
                IQueryCursor<IList> queryCursor = cache.QueryFields(fieldsQuery);

                foreach (IList fieldList in queryCursor)
                {
                    Console.WriteLine(fieldList[0]);
                }
            */


            Console.WriteLine("Welcome To My World !");

        }
    }
}
