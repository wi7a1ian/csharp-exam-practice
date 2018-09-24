using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IComparableExample
{
    class Program
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/system.icomparable%28v=vs.110%29.aspx
        /// </summary>
        static void Main(string[] args)
        {
            ArrayList temperatures = new ArrayList();
            // Initialize random number generator.
            Random rnd = new Random();

            // Generate 10 temperatures between 0 and 100 randomly. 
            for (int ctr = 1; ctr <= 10; ctr++)
            {
                int degrees = rnd.Next(0, 100);
                Temperature temp = new Temperature();
                temp.Fahrenheit = degrees;
                temperatures.Add(temp);
            }

            // Sort ArrayList.
            temperatures.Sort();

            foreach (Temperature temp in temperatures)
                Console.WriteLine(temp.Fahrenheit);
        }

        // The example displays the following output to the console (individual 
        // values may vary because they are randomly generated): 
        //       2 
        //       7 
        //       16 
        //       17 
        //       31 
        //       37 
        //       58 
        //       66 
        //       72 
        //       95
    }
}
