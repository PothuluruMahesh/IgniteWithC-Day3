using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class Person1
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Person1 [Name={Name}, Age={Age}]";
        }
    }
    class ObjectType
    {
        static void Main()
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Person1))
            };
            IIgnite ignite = Ignition.Start();

            ICache<int, Person1> cache = ignite.GetOrCreateCache<int, Person1>("Person1s");
            cache[2] = new Person1 { Name = "Mahesh", Age = 23 };
            cache[3] = new Person1 { Name = "Mahesh44", Age = 99 };
            cache[4] = new Person1 { Name = "Mahesh100", Age = 33 };
            cache[5] = new Person1 { Name = "Mahesh420", Age = 44 };
            cache[6] = new Person1 { Name = "Mahesh124", Age = 66 };

            foreach (ICacheEntry<int, Person1> cacheEntry in cache)
            {
                Console.WriteLine(cacheEntry);
            }
        }
    }
}
