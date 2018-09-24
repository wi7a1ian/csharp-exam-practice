using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AttributeTest
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple=true)]
    public class MyCustomAttribute : Attribute
    {
        public string SomeProp { get; set; }
        public string SomeValue { get; set; }
        public string SomeReadOnlyProp => $"SomeProp {SomeProp}"; // Expression Body Definition for a Property
    }

    [MyCustomAttribute(SomeProp = "Something3", SomeValue = "SomeVal3")]
    class Program
    {
        [MyCustomAttribute(SomeProp = "Something", SomeValue = "SomeVal")]
        [MyCustomAttribute(SomeProp = "Something2", SomeValue = "SomeVal2")]
        private static int MyValue = 1; // Has to be public

        static void Main(string[] args)
        {
            Type t1 = typeof(Program);
            FieldInfo fi1 = t1.GetField("MyValue", BindingFlags.Static | BindingFlags.NonPublic);
            MyCustomAttribute[] attrType = (MyCustomAttribute[])fi1.GetCustomAttributes(typeof(MyCustomAttribute), false);

            if (attrType != null)
            {
                foreach (MyCustomAttribute attrInst in attrType)
                {
                    Console.WriteLine("{2}: {0} = {1}", attrInst.SomeProp, attrInst.SomeValue, fi1.Name);
                }
            }

            attrType = (MyCustomAttribute[])t1.GetCustomAttributes(typeof(MyCustomAttribute), false);
            if (attrType != null)
            {
                foreach (MyCustomAttribute attrInst in attrType)
                {
                    Console.WriteLine("{2}: {0} = {1}", attrInst.SomeProp, attrInst.SomeValue, t1.Name);
                }
            }

            Console.ReadKey();
        }
    }
}
