using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Number literals
            var y = 0f; // f is single
            var z = 0d; // z is double
            var r = 0m; // r is decimal
            var i = 0U; // i is unsigned int
            var j = 0L; // j is long (note capital L for clarity)
            var k = 0UL; // k is unsigned long (note capital L for clarity)

            // BitConverter
            Int32 someInt = 32;
            byte[] valBytes = BitConverter.GetBytes(someInt);
            Int16 val1 = BitConverter.ToInt16(valBytes, 0);
            Int16 val2 = BitConverter.ToInt16(valBytes, 2);

            CheckedOverflowAndUnderflow();
        }

        static void CheckedOverflowAndUnderflow()
        {
            // Overflow
            try
            {
                checked
                {
                    // Narrowing conversion
                    int big = 1000000;
                    short small = (short)big;
                }
            }
            catch (OverflowException e)
            {
                //
            }

            // Underflow
            try
            {
                double big2 = -1e40;
                float small2 = (float)big2;
                if (float.IsInfinity(small2))
                    throw new OverflowException();
            }
            catch (OverflowException e)
            {
                //
            }
        }
    }
}
