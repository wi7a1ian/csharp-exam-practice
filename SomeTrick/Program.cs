using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeTrick
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommon[] sources = new ICommon[] { new SpecificTypeFirst { First = "Hello" }, new SpecificTypeSecond { Second = "World" } };

            var aggregate = new Aggregate(Console.Out);
            foreach (ICommon source in sources)
            {
                aggregate.Execute(source);
            }
        }
    }

    public class Aggregate
    {
        private TextWriter _console;

        public Aggregate(TextWriter console)
        {
            _console = console;
        }

        public void Execute(ICommon source)
        {
            // Net magic to call one of 'When' handlers
            // with matching signature
            ((dynamic)this).When((dynamic)source);

            // to tez dziala
            When((dynamic)source);
        }

        private void When(SpecificTypeFirst source)
        {
            _console.WriteLine(source.First);
        }

        private void When(SpecificTypeSecond source)
        {
            _console.WriteLine(source.Second);
        }

    }


    public class ICommon
    {

    }

    public class SpecificTypeFirst : ICommon
    {
        public string First { get; set; }
    }

    public class SpecificTypeSecond : ICommon
    {
        public string Second { get; set; }
    }

}
