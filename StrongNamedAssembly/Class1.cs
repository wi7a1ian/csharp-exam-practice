﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongNamedAssembly
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/wd40t7ad%28v=vs.110%29.aspx
    /// 
    /// A strong named assembly is generated by using the private key that corresponds to the public key distributed with the assembly, and the assembly itself. The assembly includes the assembly manifest, which contains the names and hashes of all the files that make up the assembly. Assemblies that have the same strong name should be identical.
    /// You can strong-name assemblies by using Visual Studio or a command-line tool.
    /// When a strong-named assembly is created, it contains the simple text name of the assembly, the version number, optional culture information, a digital signature, and the public key that corresponds to the private key used for signing.
    /// 
    /// http://msdn.microsoft.com/en-us/library/xc31ft41(v=vs.110).aspx
    /// 
    /// http://msdn.microsoft.com/en-us/library/k5b5tt23(v=vs.110).aspx
    /// The Strong Name tool (Sn.exe) helps sign assemblies with strong names. Sn.exe provides options for key management, signature generation, and signature verification.
    /// 
    /// Assigning strong name: Creating a cryptographic key pair using the Strong Name tool (Sn.exe) and assigning that key pair to the assembly using either a command-line compiler or the Assembly Linker (Al.exe). The Windows Software Development Kit (SDK) provides both Sn.exe and Al.exe.
    /// 
    /// http://msdn.microsoft.com/en-us/library/6f05ezxy(v=vs.110).aspx
    /// 
    /// 
    /// DisplayName used when referencing in runtine:
    ///    <assembly name>, <version number>, <culture>, <public key token>
    /// For example:
    ///    myDll, Version=1.1.0.0, Culture=en, PublicKeyToken=03689116d3a4ae33 
    ///    
    /// How to: Reference a Strong-Named Assembly
    /// http://msdn.microsoft.com/en-us/library/s1sx4kfb(v=vs.110).aspx
    /// </summary>
    public class Class1
    {
    }
}