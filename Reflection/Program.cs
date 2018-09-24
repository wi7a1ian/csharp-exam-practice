using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dog = new Dog();

            // At compile time
            Type t1 = typeof(Dog);
            // At runtime
            t1 = dog.GetType();
            
            Console.WriteLine(string.Join(Environment.NewLine, t1.Namespace, t1.Name, t1.Assembly, t1.FullName, t1.Module));

            var newDog = (Dog)Activator.CreateInstance(t1);
            newDog = (Dog)Activator.CreateInstance<Dog>();
            newDog = (Dog)t1.GetConstructors()[0].Invoke(null);


            Console.ReadKey();
        }

    };

    internal class Dog 
    {
        private static short _objCnt = 0;
        public Dog()
        {
            Console.WriteLine("Ima dog WOOF WOOF #{0}", ++_objCnt);
        }
    };

}
