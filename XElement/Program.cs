using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XElementTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XNamespace aw = "http://www.adventure-works.com";
            XElement xmlTree1 = new XElement(aw + "Root",
                new XAttribute("name", "dupa"),
                new XElement(aw + "Child1", 1),
                new XElement(aw + "Child2", 2),
                new XElement(aw + "Child3", 3, new XAttribute(aw + "name", "dupa")),
                new XElement(aw + "Child4", 4),
                new XElement(aw + "Child5", 5),
                new XElement(aw + "Child6", 6)
            );
            Console.WriteLine(xmlTree1);

            XElement xmlTree2 = new XElement(aw + "Root",
                from el in xmlTree1.Elements()
                where ((int)el >= 3 && (int)el <= 5)
                select el
            );
            Console.WriteLine(xmlTree2);
            Console.ReadKey();
        }
    }
}
