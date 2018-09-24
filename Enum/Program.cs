using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumTest
{
    [FlagsAttribute]
    public enum Group
    {
        Users = 1,
        Supervisors = 2,
        Managers = 4,
        Admins = 8
    }

    public enum Color : short
    {
        Red = 1,
        Green = 2,
        Blue = 4,
    }

    class Program
    {
        static void Main(string[] args)
        {
            Group userGroup = Group.Supervisors;

            Debug.Assert(userGroup == Group.Supervisors);
            Debug.Assert((userGroup & Group.Supervisors) == Group.Supervisors);

            for (int val = 0; val < 4; val++)
                Console.WriteLine("{0,3} - {1:G}", val, (Group)val);

            for (int val = 0; val < 4; val++)
                Console.WriteLine("{0,3} - {1:G}", val, Enum.GetName(typeof(Group), val));

            foreach(string val in Enum.GetNames(typeof(Group)))
                Console.WriteLine("Foreach {0}", val);

            Console.WriteLine((Group)1);
            Console.WriteLine(Group.Supervisors.ToString());

            Console.WriteLine((Color)1);
            Console.WriteLine(Color.Red.ToString());

            Console.ReadKey();
        }
    }
}
