using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMe();
            Console.ReadKey();
        }

        static async void TestMe()
        {
            Console.WriteLine("Opening");
            var context = new TestDatabaseEntities();
            Console.WriteLine("Opened");
            var ts = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Reading...");
                foreach (var obj in context.TestTable)
                {
                    Console.WriteLine("{0,-5}{1,-20}{2,3}", obj.Id, obj.Name, ++obj.Value);
                }
                Console.WriteLine("...done");
            });

            await ts;

            var ts2 = Task.Factory.StartNew(() =>
            {
                var tt = new TestTable { Name = DateTime.Now.ToString(""), Value = 0 };
                context.TestTable.Add(tt);
            });

            Task.WaitAll(ts, ts2);

            await context.SaveChangesAsync();

            Console.WriteLine("Saved");
        }
    }
}
