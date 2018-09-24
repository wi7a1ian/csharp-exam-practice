using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Certificates
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try 
            { 
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, "E167AF566580EA6E0C0515CE955154D9892E4EEB", false);
                Console.WriteLine("{0} certs by thumbnail", certs.Count);

                var nameStr = "CN=Kroll Ontrack Inc., OU=Digital ID Class 3 - Microsoft Software Validation v2, O=Kroll Ontrack Inc., L=Eden Prairie, S=Minnesota, C=US";
                //nameStr = "Kroll Ontrack Inc."; // wont work
                certs = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, nameStr, false);
                Console.WriteLine("{0} certs by distinguished subject", certs.Count);
                
                nameStr = "Kroll Ontrack Inc.";
                //nameStr = "CN=Kroll Ontrack Inc., OU=Digital ID Class 3 - Microsoft Software Validation v2, O=Kroll Ontrack Inc., L=Eden Prairie, S=Minnesota, C=US"; // Won't work
                certs = store.Certificates.Find(X509FindType.FindBySubjectName, nameStr, false);
                Console.WriteLine("{0} certs by subject", certs.Count);
            }
            catch (Exception e) { }
            finally
            {
                store.Close();
            }

            Console.ReadKey();
        }
    }
}
