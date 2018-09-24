using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//
// http://www.codeproject.com/Articles/152765/Task-Parallel-Library-1-of-n
// http://www.codeproject.com/Articles/518856/Task-Parallel-Library-and-async-await-Functionalit
//

namespace TaskProgressManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTaskProgressManager tst = new TestTaskProgressManager();
            tst.progressManager.ProgressChanged += progressManager_ProgressChanged;//Set the Progress changed event
            tst.RunTask(100);
            Console.ReadKey();
            tst.cancellationTokenSource.Cancel();
            Console.ReadKey();
        }

        private static void progressManager_ProgressChanged(object sender, string e)
        {
            Console.Clear();
            Console.Write(e);
        }
    }

    internal class TestTaskProgressManager
    {
        // The basic idea is that we need to obtain a CancellationToken from a CancellationTokenSource 
        // and pass the obtained CancellationToken as one of the Task creation parameters, either via the Task constructor, 
        // or by using one of the Task.Factory.StartNew(..) method overloads.
        public CancellationTokenSource cancellationTokenSource; //Declare a cancellation token source
        public CancellationToken cancellationToken; //Declare a cancellation token object, we will populate this token from the token source, and pass it to the Task constructer.
        public Progress<string> progressManager = new Progress<string>();//Declare the object that will manage progress,and will be used to get the progress form our background thread


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception">Sometimes</exception>
        /// <param name="numericValue"></param>
        public async void RunTask(int numericValue = 100)
        {
            object[] arrObjects = new object[] { numericValue };//Declare the array of objects

            //Because Cancellation tokens cannot be reused after they have been cancelled, we need to create a new cancellation token before each start
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            using (Task<string> task = new Task<string>(new Func<object, string>(PerfromTaskAction), arrObjects, cancellationToken))//Declare and initialize the task
            {
                Console.Clear();
                Console.Write("Started Calculation...");//Signal the completion of the task

                try
                {
                    task.Start();//Start the execution of the task
                    await task;// wait for the task to finish, without blocking the main thread
                    //task.Wait(); // but this will block main thread :/
                }
                catch (AggregateException e)
                {
                    Console.Clear();
                    Console.Write("Operation cancelled because of the exception.");//Signal the completion of the task
                    foreach (Exception iE in e.InnerExceptions)
                    {
                        Console.Write("Exception: {0}", iE.Message);
                    }
                }
                finally
                {
                    if (!task.IsFaulted)
                    {
                        Console.Clear();
                        Console.Write("Successfully completed.");//Signal the completion of the task
                    }
                }
            }

        }

        private string PerfromTaskAction(object state)
        {
            object[] arrObjects = (object[])state; //Get the array of objects from the main thread
            int maxValue = (int)arrObjects[0]; //Get the maxValue integer from the array of obejcts

            StringBuilder sb = new StringBuilder();//Declare a new string builder to build the result

            for (int i = 0; i < maxValue; i++)
            {
                if (cancellationToken.IsCancellationRequested)//Check if a cancellation request is pending
                {
                    break;
                    //throw new OperationCanceledException("Operation cancelled by the user.", cancellationToken);
                }
                else
                {
                    sb.Append(string.Format("Counting Number: {0}{1}", PerformHeavyOperation(i), Environment.NewLine));//Append the result to the string builder
                    ((IProgress<string>)progressManager).Report(string.Format("Now Counting number: {0}. ", i));//Report our progress to the main thread
                }

                // Or instead of checking the IsCancellationRequested just run this:
                //cancellationToken.ThrowIfCancellationRequested();

            }

            return sb.ToString();//return the result
        }

        private int PerformHeavyOperation(int i)
        {
            System.Threading.Thread.Sleep(100);
            return i * 1000;
        }
    }
}
