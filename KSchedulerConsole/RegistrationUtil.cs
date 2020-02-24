using System;
using System.Collections.Generic;
using System.Linq;

namespace KSchedulerConsole
{
    public class RegistrationUtil
    {
        public RegistrationUtil()
        {
        }

        private static List<Seat> InitializeSeats(int num)
        {
            var seats = Enumerable.Range(1, num).Select(i => new Seat { Id = i, Name = $"WS-{i}" });
            return seats.ToList();
        }

        private static List<Employee> InitializeEmployees(int num)
        {
            var emps = Enumerable.Range(1, num).Select(i => new Employee { Id = i, Name = $"BBNO-{i}" });
            return emps.ToList();
        }

        public static (List<Seat>, List<Employee>) Seed(int numEmp, int numSeats)
        {
            return (InitializeSeats(numSeats), InitializeEmployees(numEmp));
        }
    }
}
