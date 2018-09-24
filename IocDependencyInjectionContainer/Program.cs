using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace IocDependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ISomeClass>().To<SomeClass>();


            ISomeClass samurai = kernel.Get<ISomeClass>();
            samurai.DoSomething("with Ninject!");

            Console.ReadKey();
        }
    }

    internal class SomeClass : ISomeClass
    {
        void ISomeClass.DoSomething(string sth)
        {
            Console.WriteLine("Doing something {0}", sth);
        }
    }

    internal interface ISomeClass
    {
        void DoSomething(string sth);
    }
}
