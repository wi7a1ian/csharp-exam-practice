using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            ChildClass t1 = new ChildClass();
            t1.SomeVirtualMethod("");
            ((ParentClass)t1).SomeVirtualMethod("test");

            t1.SomeFinalMethod("");
            ((ParentClass)t1).SomeFinalMethod("test");

            ParentClass t2 = new ChildClass();
            t2.SomeVirtualMethod("");
            ((ChildClass)t1).SomeVirtualMethod("test");

            t2.SomeFinalMethod("");
            ((ChildClass)t1).SomeFinalMethod("test");

            Console.ReadKey();
        }
    }

    public class ParentClass
    {
        public virtual void SomeVirtualMethod(string someParam)
        {
            Console.WriteLine("(void)ParentClass::SomeVirtualMethod");
        }

        public void SomeFinalMethod(string someParam)
        {
            Console.WriteLine("(void)ParentClass::SomeFinalMethod");
        }
    }

    public class ChildClass : ParentClass
    {
        public override void SomeVirtualMethod(string someParam)
        {
            Console.WriteLine("(void)ChildClass::SomeVirtualMethod");
        }

        public new void SomeFinalMethod(string someParam)
        {
            Console.WriteLine("(void)ChildClass::SomeFinalMethod");
        }
    }
}
