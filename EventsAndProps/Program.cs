using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsAndProps
{
    class Program
    {
        static void Main(string[] args)
        {
            var y = new SomeClass();
        }
    }

    class SomeClass
    {
        public event EventHandler<EventArgs> Renamed;
        public SomeClass()
        {
            Renamed += (sender, e) => { Console.WriteLine("Renamed1"); };
            Renamed += delegate(object sender, EventArgs e) { Console.WriteLine("Renamed2"); Thread.Sleep(1000); };
            Renamed += delegate(object sender, EventArgs e) { Console.WriteLine("Renamed3"); };
            Renamed += (sender, e) => { Console.WriteLine("Renamed4"); };
            Renamed += delegate(object sender, EventArgs e) { Console.WriteLine("Renamed5"); };

            if (Renamed != null)
                Renamed(this, new EventArgs());
        }

        protected internal bool SomeProp
        {
            get;
            private set;
        }

        protected internal bool SomeProp2
        {
            get; //protected get; // error
            set; //protected set;
        }
    }
}
