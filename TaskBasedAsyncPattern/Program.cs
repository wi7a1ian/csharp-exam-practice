using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsyncPattern
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/hh191443.aspx
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            TestTAP();
        }

        static void TestTAP()
        {
            //SomeWaitingTask();

            var res = SomeAsyncMethod(1);

            Console.WriteLine("Main Pre SomeAsyncMethod");
            Console.WriteLine("Main Post SomeAsyncMethod");

            SomeNotAwaitedAsyncTask(); // No controll, but can proceed
            SomeAwaitableAsyncTasks(); // We get back the controll but we won't wait for it

            Console.WriteLine("Main + Result" + res.Result);

            SomeAwaitableAsyncTasks().Wait(); // We will wait because it returns a task which represents the action of waiting until everything has completed
            SomeBlockingTasks(); // Will block current thread altho we didn't call .Wait()

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        // NEVER RETURN ASYNC VOID! You won't be able to wait for it, nor will you be able to catch exceptions!
        static async void SomeNotAwaitedAsyncTask()
        {
            var res = await SomeAsyncMethod(2);
        }

        // We return controll back to the caller here
        static async Task SomeAwaitableAsyncTasks()
        {
            var t1 = SomeAsyncMethod(3);
            var t2 = SomeAsyncMethod(3);

            //Task.WaitAll(t1, t2); // BAD - will block
            await Task.WhenAll(t1, t2); // OK - won't block and can be awaited, yielding control back to the caller until all tasks finish
        }

        // We return controll back to the caller here
        static async Task SomeBlockingTasks()
        {
            var t1 = SomeAsyncMethod(3);
            var t2 = SomeAsyncMethod(3);

            Task.WaitAll(t1, t2); // BAD - will block
            //await Task.WhenAll(t1, t2); // OK - won't block and can be awaited, yielding control back to the caller until all tasks finish
        }

        static async Task<int> SomeAsyncMethod(int val)
        {
            int result = 0;

            //int res3 = await SomeAsyncTask(val);
            Task<int> ts3 = SomeAsyncTask(val);

            Task<int> ts1 = new Task<int>((x) =>
            {
                Thread.Sleep(200);
                Console.WriteLine("TS1-"+val);
                return (int)x;
            }, val);
            ts1.Start();

            result += await ts1; // fork + return

            Task ts2 = Task.Factory.StartNew((x) =>
            {
                Thread.Sleep(700);
                Console.WriteLine("TS2-"+val);
                result += val;
            }, val);

            ts2.Wait(); // Without this we will not get "result" updated to 4

            return result + await ts3; // fork + return
        }

        static Task<int> SomeAsyncTask(int val)
        {
            int res3 = 0;
            Task<int> ts = Task.Factory.StartNew<int>((x) =>
            {
                Thread.Sleep(500);
                Console.WriteLine("TS3-"+val);
                res3 = (int)x;
                return (int)x;
            }, val);
            return ts;
        }

        class VolatileExplanation
        {
            /// <summary>
            /// The volatile keyword indicates that a field might be modified by multiple threads that are executing at the same time. 
            /// Fields that are declared volatile are not subject to compiler optimizations that assume access by a single thread. 
            /// This ensures that the most up-to-date value is present in the field at all times.
            /// 
            /// The volatile modifier is usually used for a field that is accessed by multiple threads without using the lock statement to serialize access.
            /// </summary>
            public volatile int _i;

            public VolatileExplanation(int i)
            {
                _i = i;
            }
        }
    }
}
