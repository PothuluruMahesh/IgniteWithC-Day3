using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Student [Name={Name}, Age={Age}]";
        }
    }
    class Filter : ICacheEntryFilter<int, Student>
    {
        public bool Invoke(ICacheEntry<int, Student> entry)
        {
            return entry.Value.Age > 30;
        }
    }


    class StudentFilter
    {
        static void Main()
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Student),
                    typeof(StudentFilter))
            };
            IIgnite ignite = Ignition.Start();

            ICache<int, Student> cache = ignite.GetOrCreateCache<int, Student>("student");
            cache[1] = new Student { Name = "Mahesh", Age = 44 };
            cache[2] = new Student { Name = "Naresh", Age = 24 };
            cache[3] = new Student { Name = "Bhvanesh", Age = 23 };
            cache[4] = new Student { Name = "Anji", Age = 34 };
            cache[5] = new Student { Name = "Arun", Age = 37 };
            cache[6] = new Student { Name = "Ramana", Age = 32 };
            cache[7] = new Student { Name = "Subbu", Age = 44 };
            cache[8] = new Student { Name = "Abba", Age = 45 };
            cache[9] = new Student { Name = "Bitta", Age = 32 };
            cache[10] = new Student { Name = "Katta", Age = 34 };
            cache[11] = new Student { Name = "Chinna", Age = 37 };
            cache[12] = new Student { Name = "Biz", Age = 8 };

            var scanQuery = new ScanQuery<int, Student>(new Filter());
            IQueryCursor<ICacheEntry<int, Student>> queryCursor = cache.Query(scanQuery);

            foreach (ICacheEntry<int, Student> cacheEntry in queryCursor)
                Console.WriteLine(cacheEntry);
        }

    }
}
