using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CastingArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList array1 = new ArrayList();
            int val1 = 3;
            int val2;
            array1.Add(val1);
            val2 = Convert.ToInt32(array1[0]);
            //val2 = ((List<int>)array1)[0];
            //val2 = ((int[])array1)[0];
        }
    }
}
