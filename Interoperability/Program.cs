using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Interoperability
{
    class Program
    {
        static void Main(string[] args)
        {
            // Two ways:
            // - COM Interop
            // - Platform Invoke (P/invoke)

            string longName = @"C:\Laboratory\Exams\CSharp\CSharpExamPractice\CryptoAes\bin\Debug";

            char[] buffer = new char[1024];
            long length = NativeMethods.GetShortPathName(longName, buffer, buffer.Length);
            string shortName = new string(buffer).Substring(0, (int)length);

            StringBuilder buffer2 = new StringBuilder(1024);
            length = NativeMethods.GetShortPathName2(longName, buffer2, buffer2.Capacity);
            string shortName2 = buffer2.ToString();


            Console.WriteLine(longName);
            Console.WriteLine(shortName);
            Console.WriteLine(shortName2);

            Console.WriteLine("Press Any key!");
            Console.ReadKey();
        }
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, EntryPoint = "GetShortPathName")]
        [return: MarshalAs(UnmanagedType.U4)]
        internal static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);
        // We can use GetLastWin32Error to see what went wrong

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, EntryPoint = "GetShortPathName")]
        [return: MarshalAs(UnmanagedType.U4)]
        internal static extern uint GetShortPathName2(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath, 
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath, int cchBuffer);
    }
}
