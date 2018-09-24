using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatesExample
{
    class Program
    {
        // Add empty delegate to the event! Do not need to check if is null.
        static event EventHandler<Action> someEvent = delegate { }; 

        static void Main(string[] args)
        {
            // Events
            Action eve = () => { Console.WriteLine("Invoked event!"); };
            someEvent += (_, a) => a();
            //if (someEvent != null)
                someEvent(new { }, eve);

            // Action delegate
            Action del1 = () => { Console.WriteLine("del1"); };
            Action del2 = () => { Console.WriteLine("del2"); };
            Action del3 = del1 + del2 + del1;
            del3();

            

            // Action delegate #2
            Action<object> del4 = (x) => { Console.WriteLine(x); };

            // Function delegate
            Func<int, double> returnDouble = (i) => Convert.ToDouble(i);

            // Anonymous
            Func<int, double> returnDouble2 = /* here -> */delegate(int i) { return Convert.ToDouble(i); };

            // Lambda expression
            Func<int, double> returnDouble3 = /* here -> */ (i) => Convert.ToDouble(i);

            // Lambda statement
            Func<int, double> returnDouble4 = /* here -> */ (i) => { return Convert.ToDouble(i); };

            // Async lambda
            Action<string> returnDouble4Async = async (x) => await Task.Factory.StartNew(del4, (object)x);
            returnDouble4Async("Async lambda!");

            // Try callback (.NET 2.0 ?)
            CreatingCalbackFromMethod();

            Console.ReadKey();
        }

        delegate void SomeCallbackMethodDelegate();
        static void CreatingCalbackFromMethod()
        {
            var callback = new SomeCallbackMethodDelegate(delegate() { Console.WriteLine("Callback me!"); Thread.Sleep(1000); });
            
            callback.Invoke(); // #1

            IAsyncResult res = callback.BeginInvoke((_) => Console.WriteLine(_.AsyncState), "Callback done!"); // #2
        }


    }
}
