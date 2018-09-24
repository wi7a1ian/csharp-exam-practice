using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamiv
{
    class Program
    {
        static void Main(string[] args)
        {

            dynamic oo = new { Hi = "somethibg", Lol = "else" }; // Anonymous class
            object oo2 = new { Hi = "somethibg", Lol = "else" };
            dynamic oo3 = new ExpandoObject();//not new Object();, Represents an object whose members can be dynamically added and removed at run time.
            oo3.Hi = "something";

            Console.WriteLine(oo.Hi);
            //Console.WriteLine(oo2.Hi);
            Console.WriteLine(oo3.Hi);
            SomeFunc(oo);
            SomeFunc(oo2);
            SomeFunc(oo3);

            Console.ReadKey();

            dynamic expando = new ExpandoObject();
            expando.Address = new ExpandoObject();
            expando.Address.State = "WA";
            Console.WriteLine(expando.Address.State);
            
            // ExpandoObject implements INotifyPropertyChanged interface which gives you more control over properties than a dictionary.
            
            ((IDictionary<String, Object>)expando).Remove("Address"); // Removing

            // Listening for changes
            ((INotifyPropertyChanged)expando).PropertyChanged +=
            new PropertyChangedEventHandler(HandlePropertyChanges);
            expando.Name = "John Smith";
        }

        static void SomeFunc(dynamic obj)
        {
            Console.WriteLine(obj.Hi);
        }

        private static void HandlePropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("{0} has changed.", e.PropertyName);
        }
    }
}
