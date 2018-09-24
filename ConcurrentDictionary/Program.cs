using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var dick = new ConcurrentDictionary<string, int>();

            dick.AddOrUpdate("dick", 1, (k,v) => v + 1);
            dick.AddOrUpdate("dick", 1, (k,v) => v + 1);
            dick.AddOrUpdate("dick", 1, (k,v) => v + 1);
            dick.AddOrUpdate("dick", 1, (k,v) => v + 1);

            int val = dick.GetOrAdd("dick", 1);

        }
    }
}
