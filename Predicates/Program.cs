using Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var party = new Party();            
            var employees = new List<Employee>
            {
                new Employee(1, 20),
                new Employee(2, 25),
                new Employee(3, 30),
                new Employee(4, 35),
            };

            party.Invite(employees.Where(
                PredicatesLang.When<int, Employee>(e => e.Age)
                .Is(new GreaterThan(20)
                //.And(LessThan(70))
                )
            ));
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
        public List<Employee> InvitationList { get; set; }

        public void Invite(List<Employee> employees)
        {
            InvitationList.AddRange(employees);
        }
    }
}
