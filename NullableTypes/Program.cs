using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Nullable<int> i_ = 1;
            Nullable<int> j_ = null;

            Console.WriteLine("i: HasValue={0}, Value={1}", i_.HasValue, i_.Value);
            Console.WriteLine("j: HasValue={0}, Value={1}", j_.HasValue, j_.GetValueOrDefault());

            // Implicit conversion from System.Int32 to Nullable<Int32>
            int? i = 5;

            // Implicit conversion from 'null' to Nullable<Int32>
            int? j = null;

            // Explicit conversion from Nullable<Int32> to non-nullable Int32
            int k = (int)i;

            // Casting between nullable primitive types
            Double? x = 5; // Implicit conversion from int to Double? (x is 5.0 as a double)
            Double? y = j; // Implicit conversion from int? to Double? (y is null)

            // Unary operators (+ ++ - -- ! ~)
            i++; // i = 6
            j = -j; // j = null

            // Binary operators (+ - * / % & | ^ << >>)
            i = i + 3; // i = 9
            j = j * 3; // j = null;

            // Equality operators (== !=)
            if (i == null) { /* no */ } else { /* yes */ }
            if (j == null) { /* yes */ } else { /* no */ }
            if (i != j) { /* yes */ } else { /* no */ }

            // Comparison operators (< > <= >=)
            if (i > j) { /* no */ } else { /* yes */ }
        }

        static void CoalescingOperator()
        {
            int? i = null;
            int j;

            if (i.HasValue)
                j = i.Value;
            else
                j = 0;

            //The above code can also be written using Coalescing operator:
            j = i ?? 0;

            // Test data
            Func<string> GetFileName = () => null;
            string suppliedTitle = null;

            //Other Examples:
            string pageTitle = suppliedTitle ?? "Default Title";
            string fileName = GetFileName() ?? string.Empty;

            //The Coalescing operator is also quite useful in aggregate function 
            //while using linq. For example,
            int?[] numbers = { };
            int total = numbers.Sum() ?? 0;

            // Many times it is required to Assign default, if not found in a list.
            //Customer customer = db.Customers.Find(customerId) ?? new Customer();

            //It is also quite useful while accessing objects like QueryString, 
            //Session, Application variable or Cache.
            //string username = Session["Username"] ?? string.Empty;
            //Employee employee = GetFromCache(employeeId) ?? GetFromDatabase(employeeId);
        }
    }
}
