using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using StrongNamedAssembly;
using System.Reflection;

namespace StrongNamedAssemblyConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(typeof(StrongNamedAssembly.Class1));

            // Two ways:
            // Reference DLL in this project
            // Do not reference and allow the system to load DLL from GAC (need to be installed first)

            // Local assembly vs GAC
            // If both assemblies are strong-named (signed), the CLR will always load from the GAC.

            // Fully qualified name is needed
            Assembly myDll = Assembly.Load("StrongNamedAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7124b119861ddade");
            //myDll = Assembly.Load("StrongNamedAssembly"); // will fail
            Console.WriteLine(myDll);


            Console.ReadKey();
        }
    }
}
