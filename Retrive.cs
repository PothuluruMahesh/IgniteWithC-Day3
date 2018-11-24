using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class Retrive
    {
        static void Main(string[] args)
        {
            IIgnite ignite = Ignition.Start();
            ICache<int, string> cache = ignite.GetOrCreateCache<int, string>("test");
            if (cache.PutIfAbsent(1, "Hello, World!"))
                Console.WriteLine("Added a value to the cache!");
            else
                Console.WriteLine(cache.Get(1));
        }
    }
}
