/*
 * The returned Task<TResult> will not be scheduled for execution until the current task has completed, whether it completes due to running to completion successfully, faulting due to an unhandled exception, or exiting out early due to being canceled.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

class ContinuationSimpleDemo
{
    // Demonstrated features: 
    // 		Task.Factory 
    //		Task.ContinueWith() 
    //		Task.Wait() 
    // Expected results: 
    // 		A sequence of three unrelated tasks is created and executed in this order - alpha, beta, gamma. 
    //		A sequence of three related tasks is created - each task negates its argument and passes is to the next task: 5, -5, 5 is printed. 
    //		A sequence of three unrelated tasks is created where tasks have different types. 
    // Documentation: 
    //		http://msdn.microsoft.com/en-us/library/system.threading.tasks.taskfactory_members(VS.100).aspx 
    static void Main()
    {
        Action<string> action =
            (str) =>
                Console.WriteLine("Task={0}, str={1}, Thread={2}", Task.CurrentId, str, Thread.CurrentThread.ManagedThreadId);

        Func<int, int> negate =
            (n) =>
            {
                Console.WriteLine("Task={0}, n={1}, -n={2}, Thread={3}", Task.CurrentId, n, -n, Thread.CurrentThread.ManagedThreadId);
                return -n;
            };

        Action<string> except = 
            (str) => 
            { 
                throw new Exception(str); 
            };

        Action<Task> exceptListener =
            (tsk) =>
            {
                Console.WriteLine((!tsk.IsFaulted) ? "No Exception." : (tsk.Exception.InnerExceptions.Count + " exception(s) were thrown."));
            };



        // Creating a sequence of action tasks (that return no result).
        Console.WriteLine("Creating a sequence of action tasks (that return no result)");
        Task.Factory.StartNew(() => action("alpha"))
            .ContinueWith(antecendent => action("beta"))        // Antecedent data is ignored
            .ContinueWith(antecendent => action("gamma"))
            .Wait();

        // Creating a sequence of function tasks where each continuation uses the result from its antecendent
        Console.WriteLine("\nCreating a sequence of function tasks where each continuation uses the result from its antecendent");
        Task<int>.Factory.StartNew(() => negate(5))
            .ContinueWith(antecendent => negate(antecendent.Result))		// Antecedent result feeds into continuation
            .ContinueWith(antecendent => negate(antecendent.Result))
            .Wait();

        // Creating a sequence of tasks where you can mix and match the types
        Console.WriteLine("\nCreating a sequence of tasks where you can mix and match the types");
        Task<int>.Factory.StartNew(() => negate(6))
            .ContinueWith(antecendent => action("x"))
            .ContinueWith(antecendent => negate(7))
            .Wait();


        // Detecting exception from previous task #1
        Task.Factory.StartNew(() => except("Throw Exception!"))
            .ContinueWith(antecendent => exceptListener(antecendent)); // You need to access tsk.Exception otherwise it will be unhandled excpt!
        Task.Factory.StartNew(delegate() { except("Throw Exception!"); })
            .ContinueWith(antecendent => exceptListener(antecendent)); // You need to access tsk.Exception otherwise it will be unhandled excpt!

        // Detecting exception from previous task #2
        var t1 = Task.Factory.StartNew(() => except("Throw Exception!"))
            .ContinueWith(antecendent => exceptListener(antecendent));
        try
        {
            t1.Wait();
        }
        catch { }

        // Handling exception from previous task #1
        var t2 = Task.Factory.StartNew(() => except("Throw Exception!"));
        var t2_l1 = t2.ContinueWith(antecendent => exceptListener(antecendent), TaskContinuationOptions.OnlyOnFaulted);
        var t2_l2 = t2.ContinueWith(antecendent => negate(5), TaskContinuationOptions.NotOnFaulted);
        Task.WaitAny(t2_l1, t2_l2);

        // Handling exception from previous task #2
        var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;
        var t3 = Task.Factory.StartNew(() => except("Throw Exception!"));
        var t3_l1 = t3.ContinueWith(antecendent => exceptListener(antecendent), token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current); //TaskScheduler.FromCurrentSynchronizationContext());
        var t3_l2 = t3.ContinueWith(antecendent => negate(5), token, TaskContinuationOptions.NotOnFaulted, TaskScheduler.Current); //TaskScheduler.FromCurrentSynchronizationContext());
        Task.WaitAny(t3_l1, t3_l2);

        /*
        The Task class itself has several methods/properties that cause a AggegrateExeption to be observed; some of these are as follows:
            Wait()
            Result
        When your code makes use of these, you are effectively saying, yes I am interested in observing any AggregateException that occurs. 
        Throughout the rest of this article, I will refer to these special methods/properties as trigger methods.

        One important thing to note is that if you do not use one of the trigger methods such as Wait()/Result etc., 
        TPL will not escalate any AggegateException as there is deemed to be nothing observing the AggregateException, so an unhandled Exception will occur.

        This is one small gotcha when working with TPL, but it is a vitally important one.

        Anyway, now that we know that, let's have a look at different ways in which to handle Exceptions.
        */

        using (Task taskWithFactoryAndState = Task.Factory.StartNew(() => except("Throw Exception!")))
        {
            try
            {
                // Use one of the trigger methods (ie Wait() to make sure AggregateException is observed)
                taskWithFactoryAndState.Wait();
            }
            catch (AggregateException aggEx)
            {
                foreach (Exception ex in aggEx.InnerExceptions)
                {
                    Console.WriteLine(string.Format("Caught exception '{0}'",
                        ex.Message));
                }
            }
        }

        Console.Read();
    }


}