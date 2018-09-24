using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            csc.exe - compile csharp code
                - Compiles File.cs producing File.dll:
                  csc /target:library File.cs
                - Compiles all the C# files in the current directory producing a debug version of File2.dll. No logo and no warnings are displayed:
                  csc /target:library /out:File2.dll /warn:0 /nologo /debug *.cs

            Sn.exe -k assemblyKey.snk
            
            AL.exe - All Visual Studio compilers produce assemblies. However, if you have one or more modules (metadata without a manifest), you can use Al.exe to create an assembly with the manifest in a separate file.
            The following command creates an executable file t2a.exe with an assembly from the t2.netmodule module. The entry point is theMain method in MyClass.
                al t2.netmodule /target:exe /out:t2a.exe /main:MyClass.Main
            AL.exe /out:Assembly1.dll /keyfile assemblyKey.snk
            
            EdmGen.exe is a command-line tool used for working with Entity Framework model and mapping files. You can use the EdmGen.exe tool to do the following:
            
            ngen.exe - generates native code for performance improvement
             
            
            */
        }
    }
}
