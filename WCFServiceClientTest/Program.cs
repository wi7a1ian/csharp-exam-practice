using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serv = new ServiceReference1.MyServiceClient();
            Console.WriteLine("Works? 5={0}", serv.GetJsonData("5"));
            Console.ReadKey();
        }
    }
}
