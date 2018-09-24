using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlObjectSerializerTest
{
    public class Test
    {
        private void WriteObjectWithInstance(XmlObjectSerializer xm, Company graph, string fileName)
        {
            Console.WriteLine(xm.GetType());

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                // #1
                //xm.WriteObject(writer, graph);
                
                // #2
                xm.WriteStartObject(writer, graph);
                xm.WriteObjectContent(writer, graph);
                xm.WriteEndObject(writer);
            }

            Console.WriteLine("Done writing {0}", fileName);
        }

        private void WriteObjectWithInstance(DataContractJsonSerializer xm, Company graph, string fileName)
        {
            Console.WriteLine(xm.GetType());

            using (var fs = new FileStream(fileName, FileMode.Create))
            using (var ms = new MemoryStream())
            using (var binary = XmlDictionaryWriter.CreateBinaryWriter(ms))
            using (var sr = new StreamReader(ms, Encoding.UTF8, true))
            {
                xm.WriteObject(binary, graph);
                binary.Flush();
                //binary.WriteEndElement();
                //binary.WriteEndDocument();

                var bytes = ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);
                string outStr = sr.ReadToEnd();
                ms.CopyTo(fs);

                /*byte[] bytes = new byte[ms.Length];
                ms.Read(bytes, 0, (int)ms.Length);
                fs.Write(bytes, 0, bytes.Length);*/
            }

            Console.WriteLine("Done writing {0}", fileName);
        }

        private void Run()
        {
            // Create the object to write to a file.
            Company graph = new Company();
            graph.Name = "cohowinery.com";

            // Create a DataContractSerializer and a NetDataContractSerializer. 
            // Pass either one to the WriteObjectWithInstance method.
            DataContractSerializer dcs = new DataContractSerializer(typeof(Company));
            NetDataContractSerializer ndcs = new NetDataContractSerializer();

            var jsettings = new DataContractJsonSerializerSettings();
            jsettings.UseSimpleDictionaryFormat = true;
            DataContractJsonSerializer jdcs = new DataContractJsonSerializer(typeof(Company), jsettings);

            WriteObjectWithInstance(dcs, graph, @"datacontract.xml");
            WriteObjectWithInstance(jdcs, graph, @"jsondatacontract.js");
            WriteObjectWithInstance(ndcs, graph, @"netDatacontract.xml");
        }

        [DataContract(Name = "SerialziedCompany", Namespace = "http://www.contoso.com")]
        public class Company
        {
            [DataMember(Name="CompanyName")]
            public string Name;

            [DataMember(IsRequired=false)]
            public string Option1;

            [DataMember(EmitDefaultValue = false)]
            public string Option2 = "Visible";

            [DataMember(EmitDefaultValue = false)]
            public string Option4; // By default, not visible in the serialzied XML object

            [DataMember(EmitDefaultValue = true, Order = 10)]
            public string Option3 = "Visible";

        }

        static void Main()
        {
            try
            {
                Console.WriteLine("Starting");
                Test t = new Test();
                t.Run();
                Console.WriteLine("Done");
                Console.ReadLine();
            }

            catch (InvalidDataContractException iExc)
            {
                Console.WriteLine("You have an invalid data contract: ");
                Console.WriteLine(iExc.Message);
                Console.ReadLine();
            }

            catch (SerializationException serExc)
            {
                Console.WriteLine("There is a problem with the instance:");
                Console.WriteLine(serExc.Message);
                Console.ReadLine();
            }

            //catch (QuotaExceededException qExc)
            //{
            //    Console.WriteLine("The quota has been exceeded");
            //    Console.WriteLine(qExc.Message);
            //    Console.ReadLine();
            //}
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine(exc.ToString());
                Console.ReadLine();
            }
        }
    }
}
