using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelClassExample
{
    class Program
    {
        static void Main(string[] args)
        {
            double result = 0d;

            // #1
            // Here we call same method several times in parallel.
            Parallel.For(0, 1000, i =>
            {
                result += DoIntensiveCalculations();
            });

            // Print the result
            Console.WriteLine("The result is {0}", result); // Wrong :(

            result = 0d;

            // #2
            // Here we call same method several times in parallel.
            int result2 = 0;
            Parallel.For(0, 1000, i =>
            {
                Interlocked.Add(ref result2, (int)DoIntensiveCalculations());
            });

            // Print the result
            Console.WriteLine("The result is {0}", result2); 

            result = 0d;

            // #3
            // Here we call same method several times.
            //for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            Parallel.For(0, 1000,
                // Func<TLocal> localInit,
                () => 0d,
                    // Func<int, ParallelLoopState, TLocal, TLocal> body,
                (i, state, interimResult) => interimResult + DoIntensiveCalculations(),
                    // Final step after the calculations
                    // we add the result to the final result
                    // Action<TLocal> localFinally
                (lastInterimResult) => result += lastInterimResult
            );

            // Print the result
            Console.WriteLine("The result is {0}", result);

            // #4
            var actions = new Action[1000];
            for (int i = 0; i < 1000; i++)
            {
                actions[i] = () => DoIntensiveCalculations();
            }

            var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            //OPTIONS.TASKSCHEDULER = NEW LIMITEDCONCURRENCYLEVELTASKSCHEDULER(CONCURRENCYLEVEL); -- WE CAN ACHIEVE THE SAME RESULT WITH THIS
            Parallel.Invoke(options, actions);


            Console.ReadKey();
        }

        static double DoIntensiveCalculations()
        {
            Thread.Sleep(100);
            return 1;
        }
    }
}
