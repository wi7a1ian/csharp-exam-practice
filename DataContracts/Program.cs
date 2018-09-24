//#define CONTRACTS_FULL 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;


namespace DataContracts
{

    class Program
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/dd264808(v=vs.110).aspx
        /// 
        /// http://research.microsoft.com/en-us/projects/contracts/
        /// 
        /// You need a plugin to run it.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Contract.Requires<ArgumentException>(args.Length == 0, "There are some aruments in Main!");

            // Do something
            // Do something
            // Do something
            // Do something

            //Contract.EnsuresOnThrow<ArgumentNullException>(args != null, "Yuh! We've tried to return null value");


            Rational r = new Rational(1, 0);
            Console.WriteLine("{0}", r.Denominator);

            Console.ReadKey();
        }
    }

    public class Rational
    {

        int numerator;
        int denominator;

        public Rational(int numerator, int denominator)
        {
            Contract.Requires<ArgumentException>( denominator != 0);

            this.numerator = numerator;
            this.denominator = denominator;
        }

        public int Denominator
        {
            get {
                Contract.Ensures( Contract.Result<int>() != 0 );

                return this.denominator;
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant() 
        {
            Contract. Invariant ( this.denominator != 0 );
        }
    }
}
