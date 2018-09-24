using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace AssemblyLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("Kroll.CDG.Fixer.Utils.ConcurrentFileCopy.dll");
            Assembly asm1 = Assembly.Load(bytes);
            Assembly asm2 = Assembly.LoadFrom("Kroll.CDG.Fixer.Utils.ConcurrentFileCopy.dll");

            // You cannot execute code from an assembly loaded into the reflection-only context. To execute code, the assembly must be loaded into the execution context as well, using the Load method.
            // Dependencies are not automatically loaded into the reflection-only context.
            Assembly asm3 = Assembly.ReflectionOnlyLoad(bytes);
            //Assembly asm4 = Assembly.ReflectionOnlyLoadFrom("Kroll.CDG.Fixer.Utils.ConcurrentFileCopy.dll");

            foreach (var type in asm2.GetTypes())
            //foreach (var type in asm3.GetTypes())
            {
                Console.WriteLine("Assembly {0}", type.ToString());
            }
            


            // Assembly.ReflectionOnlyLoad() is good when you need to check assembly header properties such as version, public key etc. 
            // No actual code could be executed from the assembly and no dependencies are being linked in.
            // Mostly used in plugin-based systems with dynamic assembly updates - to check if there's an actual need to call Assembly.Load() since ReflectionOnlyLoad has way smaller footprint than full-scaled Load().
            
            // The CLR needs to read the metadata from the assembly the type itself is defined in as well load
            // the metadata from every assembly the type's ancestors are defined in. So, you need to call Assembly.ReflectionOnlyLoad
            // on all assemblies that define types that are ancestors of the types you're loading.

            Console.ReadKey();
        }
    }
}
