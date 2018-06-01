

namespace Predicates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Predicates.PredicatesLang;
    using static System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
            
            var party = new Party();            
            var employees = new List<Employee>
            {
                new Employee(1, 20),
                new Employee(2, 25),
                new Employee(3, 30),
                new Employee(4, 35),
                new Employee(5, 70),
                new Employee(6, 73)
            };

            var query = When<int, Employee>(e => e.Age).Is(GreaterThan(20).And(LessThan(70)));
            party.Invite(employees.Where(query));


            party.InvitationList.ForEach(i => WriteLine($"Id: {i.Id}; Age:{i.Age};"));
            ReadKey();
        }
    }

    public class Employee
    {
        public Employee(int id, int age)
        {
            Id = id;
            Age = age;
        }

        public int Id { get; set; }

        public int Age { get; set; }
    }

    public class Party
    {
        public Party()
        {
            InvitationList = new List<Employee>();
        }
        public List<Employee> InvitationList { get; set; }

        public void Invite(IEnumerable<Employee> employees)
        {
                InvitationList.AddRange(employees);
        }
    }
}
