using System;
namespace KSchedulerConsole
{
    public class Employee
    {
        public Employee()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public Seat seat { get; set; }

        public override string ToString()
        {
            return $"{Id} -- {Name} -- {seat?.ToString()}";
        }
    }
}
