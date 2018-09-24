using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            //
            // NOTE: You can also handle an exception with use of ContinueWith()
            //
            //

            //# COMException wont be treated as Exception!
            //# AccessViolationException wont be caugcht by default!

            // create the task
            Task<List<int>> taskWithFactoryAndState =
            //Task.Factory.StartNew<List<int>>((stateObj) =>
            new Task<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i <= (int)stateObj; i++)
                {
                    ints.Add(i);
                    
                    Task childTask = new Task((iter) =>
                    {
                        if ((int)iter > 100)
                        {
                            InvalidOperationException ex =
                                new InvalidOperationException("oh no its > 100");
                            ex.Source = "taskWithFactoryAndState";
                            throw ex;
                        }
                    }, i, TaskCreationOptions.AttachedToParent);
                    childTask.Start();
                }
                return ints;
            }, 101);

            try
            {
                taskWithFactoryAndState.Start();

                // Wait function will throw the exceptions from the child tasks.
                taskWithFactoryAndState.Wait();
                if (!taskWithFactoryAndState.IsFaulted)
                {
                    Console.WriteLine(string.Format("managed to get {0} items",
                        taskWithFactoryAndState.Result.Count));
                }
            }
            catch (AggregateException aggEx)
            {
                aggEx.Handle(HandleException); // Invokes a handler on each Exception contained by this aggregation
            }
            finally
            {
                taskWithFactoryAndState.Dispose();
            }

            Console.WriteLine("All done, press Enter to Quit");

            Console.ReadLine();
        }

        private static bool HandleException(Exception ex)
        {
            if (ex is AggregateException)
            {
                (ex as AggregateException).Handle(HandleException);
                return true; // Exception was handled
            }
            else
            {
                if (ex is InvalidOperationException)
                {
                    Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
