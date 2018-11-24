using Apache.Ignite.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class LINQExample
    {
        static void Main(string[] args)
        {
            IQueryable<ICacheEntry<int, Person>> persons = personCache.AsCacheQueryable();
            IQueryable<ICacheEntry<int, Organization>> orgs = orgCache.AsCacheQueryable();

            // Simple filtering
            IQueryable<ICacheEntry<int, Person>> qry = persons.Where(e => e.Value.Age > 30);

            // Fields query
            IQueryable<string> fieldsQry = persons
                .Where(e => e.Value.Age > 30)
                .Select(e => e.Value.Name);

            // Aggregate
            int sum = persons.Sum(e => e.Value.Age);

            // Join
            IQueryable<string> join = persons
                .Join(orgs.Where(org => org.Value.Name == "Apache"),
                    person => person.Value.OrgId,
                    org => org.Value.Id,
                    (person, org) => person.Value.Name);

            // Join with query syntax
            var join2 = from person in persons
                        from org in orgs
                        where person.Value.OrgId == org.Value.Id && org.Value.Name == "Apache"
                        select person.Value.Name;
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!Welcome to My World!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}
