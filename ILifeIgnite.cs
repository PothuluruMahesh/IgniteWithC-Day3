using Apache.Ignite.Core.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2Example
{
    class ILifeIgnite
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ignite ILifeCycle is used perform some operation before or after Node Start");
        }
    }
    class LifecycleExampleHandler : ILifecycleHandler
    {
        public void OnLifecycleEvent(LifecycleEventType evt)
        {
            if (evt == LifecycleEventType.AfterNodeStart)
                Started = true;
            else if (evt == LifecycleEventType.AfterNodeStop)
                Started = false;
        }

        public bool Started { get; private set; }
    }
}
