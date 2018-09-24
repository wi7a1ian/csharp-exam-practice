using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOuterJoin
{
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StateID { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // In order to use LINQ your list objects must implement IEnumerable<T> or IQueryable<T>

            List<Employee> emplyees = new List<Employee>()
            {
                new Employee() {FirstName="Kazik", LastName="Staszewski", StateID = 0},
                new Employee() {FirstName="Kazik2", LastName="Staszewski", StateID = 1},
                new Employee() {FirstName="Kazik3", LastName="Staszewski", StateID = 0},
                new Employee() {FirstName="Kazik4", LastName="Staszewski", StateID = 1},
                new Employee() {FirstName="Kazik5", LastName="Staszewski", StateID = 10}
            };

            List<State> states = new List<State>()
            {
                new State(){StateID=0, StateName="Oklachoma"},
                new State(){StateID=1, StateName="Minnesota"},
                new State(){StateID=1, StateName="Kansas"},
                new State(){StateID=19, StateName="Wielkopolska"}
            };

            // Outer join
            var employeeByState = from e in emplyees
                                  join s in states
                                    on e.StateID equals s.StateID
                                      //on new {StateID1 = e.StateID,StateID2 = e.StateID} equals new {StateID1 = s.StateID,StateID2 = s.StateID} // Composite key
                                    into employeeGroup
                                  from item in employeeGroup.DefaultIfEmpty(new State { StateID = 0, StateName = "" })
                                  orderby item.StateName descending, e.FirstName ascending
                                  select new { e.FirstName, item.StateName }; // Projection

            foreach (var emp in employeeByState)
                Console.WriteLine("{1}, {0}", emp.FirstName, emp.StateName);

            // Grouping
            var emploeeGroupByState = from e in emplyees
                                      where e.LastName == "Staszewski" || e.LastName== "Nic"
                                      group e by e.StateID; // NOTE: no select
                                      //into g select new {Key = g.Key, SumOfNumbers = g.Sum()}

            foreach (var empGroup in emploeeGroupByState)
                foreach (var emp in empGroup)
                    Console.WriteLine("{0}: {1} {2}", emp.StateID, emp.FirstName, emp.LastName);

            // Method based Linq
            var something = emplyees.Where(x => x.StateID == 1).Select(x => new { Sth = x.FirstName, Sth2 = x.LastName });
            var something2 = emplyees.Join(states, e => e.StateID, s => s.StateID, (e, s) => new { e.FirstName, s.StateName });
            var something3 = emplyees.GroupBy(e => e.StateID).Skip(5).Take(1); // group and take 6th element

            Console.ReadKey();
        }
    }
}
