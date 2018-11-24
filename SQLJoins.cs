using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Cache.Query;

namespace Day2Example
{
    class Person
    {
        [QuerySqlField]
        public string Name { get; set; }

        [QuerySqlField]
        public int Age { get; set; }

        [QuerySqlField]
        public int OrgId { get; set; }
    }
    class Organization
    {
        [QuerySqlField]
        public string Name { get; set; }

        [QuerySqlField]
        public int Id { get; set; }
    }
    class SQLJoins
    {
        static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Person),typeof(Organization))
            };
            IIgnite ignite = Ignition.Start();

            ICache<int, Person> personCache = ignite.GetOrCreateCache<int, Person>(new CacheConfiguration("persons", typeof(Person)));

            var orgCache = ignite.GetOrCreateCache<int, Organization>(new CacheConfiguration("orgs", typeof(Organization)));

            personCache[1] = new Person { Name = "John Doe", Age = 27, OrgId = 1 };
            personCache[2] = new Person { Name = "Jane Moe", Age = 43, OrgId = 2 };
            personCache[3] = new Person { Name = "Ivan Petrov", Age = 59, OrgId = 2 };

            orgCache[1] = new Organization { Id = 1, Name = "Contoso" };
            orgCache[2] = new Organization { Id = 2, Name = "Apache" };

            var fieldsQuery = new SqlFieldsQuery(
                "select Person.Name from Person " +
                "join \"orgs\".Organization as org on (Person.OrgId = org.Id) " +
                "where org.Name = ?", "Apache");

            foreach (var fieldList in personCache.QueryFields(fieldsQuery))
            {
                Console.WriteLine(fieldList[0]);
            }
        }
    }
}
