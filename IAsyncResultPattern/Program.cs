using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

// http://msdn.microsoft.com/en-us/library/jj152938(v=vs.110).aspx

// The .NET Framework provides three patterns for performing asynchronous operations:

// Asynchronous Programming Model (APM) pattern (also called the IAsyncResult pattern), where asynchronous operations 
// require Begin and End methods (for example, BeginWrite and EndWrite for asynchronous write operations). 
// This pattern is no longer recommended for new development. For more information, see Asynchronous Programming Model (APM).

// Event-based Asynchronous Pattern (EAP), which requires a method that has the Async suffix, and also requires one or more events, 
// event handler delegate types, and EventArg-derived types. EAP was introduced in the .NET Framework 2.0. It is no longer recommended for new development. 
// For more information, see Event-based Asynchronous Pattern (EAP).

// Task-based Asynchronous Pattern (TAP), which uses a single method to represent the initiation and completion of an asynchronous operation. 
// TAP was introduced in the .NET Framework 4 and is the recommended approach to asynchronous programming in the .NET Framework. 
// The async and await keywords in C# and the Async and Await operators in Visual Basic Language add language support for TAP. For more information, see Task-based Asynchronous Pattern (TAP).


namespace Examples.AdvancedProgramming.AsynchronousOperations
{
    public class AsyncDemo 
    {
        // The method to be executed asynchronously. 
        public string TestMethod(int callDuration, out int threadId) 
        {
            Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }
    // The delegate must have the same signature as the method 
    // it will call asynchronously. 
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);
}


namespace Examples.AdvancedProgramming.AsynchronousOperations
{
    public class AsyncMain 
    {
        static void Main() 
        {
            // The asynchronous method puts the thread id here. 
            int threadId;

            // Create an instance of the test class.
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            // #3 Initiate the asynchronous call, passing three seconds (3000 ms) 
            // for the callDuration parameter of TestMethod; a dummy variable  
            // for the out parameter (threadId); the callback delegate; and 
            // state information that can be retrieved by the callback method. 
            // In this case, the state information is a string that can be used 
            // to format a console message.
            //IAsyncResult result = caller.BeginInvoke(3000,
            //    out threadId,
            //    new AsyncCallback(CallbackMethod),
            //    "The call executed on thread {0}, with return value \"{1}\".");


            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(3000, 
                out threadId, null, null);

            // After calling BeginInvoke you can do the following:
            // Do some work and then call EndInvoke to block until the call completes.
            // Obtain a WaitHandle using the IAsyncResult.AsyncWaitHandle property, use its WaitOne method to block execution until the WaitHandle is signaled, and then call EndInvoke.
            // Poll the IAsyncResult returned by BeginInvoke to determine when the asynchronous call has completed, and then call EndInvoke.
            // Pass a delegate for a callback method to BeginInvoke. The method is executed on a ThreadPool thread when the asynchronous call completes. The callback method calls EndInvoke.

            Thread.Sleep(0);
            Console.WriteLine("Main thread {0} does some work.",
                Thread.CurrentThread.ManagedThreadId);

            // #1 Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // #2 Poll while simulating work. 
            while (result.IsCompleted == false)
            {
                Thread.Sleep(250);
                Console.Write(".");
            }

            // Perform additional processing here. 
            
            // Call EndInvoke to retrieve the results. Will wait if you didn't do that previously.
            string returnValue = caller.EndInvoke(out threadId, result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".",
                threadId, returnValue);
        }

        // #3 The callback method must have the same signature as the AsyncCallback delegate. 
        static void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;

            // Retrieve the format string that was passed as state  
            // information. 
            string formatString = (string)ar.AsyncState;

            // Define a variable to receive the value of the out parameter. 
            // If the parameter were ref rather than out then it would have to 
            // be a class-level field so it could also be passed to BeginInvoke. 
            int threadId = 0;

            // Call EndInvoke to retrieve the results. 
            string returnValue = caller.EndInvoke(out threadId, ar);

            // Use the format string to format the output message.
            Console.WriteLine(formatString, threadId, returnValue);
        }
    }
}

/* This example produces output similar to the following:

Main thread 1 does some work.
Test method begins.
The call executed on thread 3, with return value "My call time was 3000.".
 */

