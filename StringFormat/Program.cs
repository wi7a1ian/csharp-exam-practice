using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringFormat
{
    class Program
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/0c899ak8(v=vs.110).aspx
        /// http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.110%29.aspx
        /// http://msdn.microsoft.com/en-us/library/dd260048(v=vs.110).aspx
        /// http://www.csharp-examples.net/string-format-datetime/
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("{0} {1}", "carlos", (12).ToString("###0"));
            Console.WriteLine("{0} {1:###0}", "carlos", 12);
            Console.WriteLine("{0} {1,-10:C} {1:D3}", "carlos", 12);
            Console.WriteLine("{1:C} {1:p1} {1:x} {1:X} {1:g} {1:G}", "carlos", 12);
            Console.WriteLine("{0} {1:X8}", "carlos", 12);
            Console.WriteLine("{0:N0} {0:N2} {0:N3}", 1200000m);
            Console.WriteLine("{0:00.00} {0:#.##} {0:0,0.0}", 0.4897f);
            Console.WriteLine("{0:#,#} - {0:#,#,} - {0:#,#,,} - {0:#,#,,,}", 2147483647);
            Console.WriteLine("{0:(###) ###-####}", 2147483647);
            Console.WriteLine("{0:t} - {0:d}", DateTime.Now);

            UseSecureString();

            Console.ReadKey();
        }

        /// <summary>
        /// Represents text that should be kept confidential. The text is encrypted for privacy when being used, 
        /// and deleted from computer memory when no longer needed. This class cannot be inherited.
        /// 
        /// An instance of the System.String class is both immutable and, when no longer needed, cannot be programmatically scheduled for garbage collection; 
        /// that is, the instance is read-only after it is created and it is not possible to predict when the instance will be deleted from computer memory. 
        /// Consequently, if a String object contains sensitive information such as a password, credit card number, or personal data, 
        /// there is a risk the information could be revealed after it is used because your application cannot delete the data from computer memory.
        /// 
        /// The value of an instance of SecureString is automatically encrypted when the instance is initialized or when the value is modified. 
        /// Your application can render the instance immutable and prevent further modification by invoking the MakeReadOnly method.
        /// 
        /// http://msdn.microsoft.com/en-us/library/system.security.securestring(v=vs.110).aspx
        /// </summary>
        static void UseSecureString()
        {
            // Define the string value to be assigned to the secure string. 
            string initString = "TestString";
            // Instantiate the secure string.
            SecureString testString = new SecureString();
            // Use the AppendChar method to add each char value to the secure string. 
            foreach (char ch in initString)
                testString.AppendChar(ch);

            // Display secure string length.
            Console.WriteLine("The length of the string is {0} characters.",
                              testString.Length);
        }

        static void UseStringReader()
        {
            StringBuilder sb = new StringBuilder("Allau Snackbar");
            using (StringReader sr = new StringReader(sb.ToString()))
            {
                Console.Write(sr.ReadLine());
            }
        }
    }

    
}
