using System;
using System.Collections;

interface IFoo
{
    void Foo();
}
struct MyStructure : IFoo
{
    public void Foo()
    {
    }
}
public static class Program
{
    static void MethodDoesNotBoxArguments<T>(T t) where T : IFoo
    {
        t.Foo();
    }

    static void Main(string[] args)
    {
        MyStructure s = new MyStructure();
        MethodDoesNotBoxArguments(s);
    }
}

/*
IL code doesn't contain any box instructions:

.method private hidebysig static void  MethodDoesNotBoxArguments<(IFoo) T>(!!T t) cil managed
{
  // Code size       14 (0xe)
  .maxstack  8
  IL_0000:  ldarga.s   t
  IL_0002:  constrained. !!T
  IL_0008:  callvirt   instance void IFoo::Foo()
  IL_000d:  ret
} // end of method Program::MethodDoesNotBoxArguments

.method private hidebysig static void  Main(string[] args) cil managed
{
  .entrypoint
  // Code size       15 (0xf)
  .maxstack  1
  .locals init ([0] valuetype MyStructure s)
  IL_0000:  ldloca.s   s
  IL_0002:  initobj    MyStructure
  IL_0008:  ldloc.0
  IL_0009:  call       void Program::MethodDoesNotBoxArguments<valuetype MyStructure>(!!0)
  IL_000e:  ret
} // end of method Program::Main
*/