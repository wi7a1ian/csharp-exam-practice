using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
        }


        public interface IEmployee
        {
            string Name { get; set;}
            int ID { get; set; } 
        }

        public class Employee : IEmployee
        {
            private string name;
            private int id;

            public Employee(string s, int i)
            {
                name = s;
                id = i;
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public int ID
            {
                get { return id; }
                set { id = value; }
            }
        }

        public class GenericList<T> where T : class, IEmployee
        {
            private class Node
            {
                private Node next;
                private T data;

                public Node(T t)
                {
                    next = null;
                    data = t;
                }

                public Node Next
                {
                    get { return next; }
                    set { next = value; }
                }

                public T Data
                {
                    get { return data; }
                    set { data = value; }
                }
            }

            private Node head;

            public GenericList() //constructor
            {
                head = null;
            }

            public void AddHead(T t)
            {
                Node n = new Node(t);
                n.Next = head;
                head = n;
            }

            public IEnumerator<T> GetEnumerator()
            {
                Node current = head;

                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }

            public T FindFirstOccurrence(string s)
            {
                Node current = head;
                T t = null;

                while (current != null)
                {
                    //The constraint enables access to the Name property. 
                    if (current.Data.Name == s)
                    {
                        t = current.Data;
                        break;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
                return t;
            }
        }

        public class EmployeeList<T> where T : Employee, IEmployee, System.IComparable<T>, new()
        {
            /// <summary>
            /// Generic Delegate
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="item"></param>
            public delegate void Del<U>(U item);

            /// <summary>
            /// GenericMethod using default()
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <returns></returns>
            public U GenericMethodReturningObjectUsingDefaultKeyword<U>()
            {
                return default(U);
            }
        }
    }
}
