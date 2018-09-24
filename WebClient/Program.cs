using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WebClient();
            var nvc = new NameValueCollection(){ {"gfe_rd", "cr"}, {"q", "anna+malina"}};
            Task<byte[]> tsk =  wc.UploadValuesTaskAsync(new Uri("http://www.google.pl"), "GET", nvc);

            try
            {
                Console.Write(tsk.Result);
            }
            catch (AggregateException e)
            {
                foreach (Exception exchild in e.InnerExceptions)
                {
                    Console.WriteLine(exchild.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
