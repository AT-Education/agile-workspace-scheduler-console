using System;
using System.Collections.Generic;
using System.Collections;
using Scheduler;
using System.Linq;

namespace KSchedulerConsole
{
    class Program
    {
        public static List<string> Employees = new List<string> { "A", "B", "C","D", "E", "F", "G"};
        private const int days = 5;

        static void Main(string[] args)
        {

            var (seats, emps) = RegistrationUtil.Seed(7, 5);

            int batch = 2;

            QueueUtil<Employee> qUtil;
            var prevcfh = new List<Employee>();

            var que = QueueUtil<Employee>.InitQueue(emps);
            qUtil = new QueueUtil<Employee>(que);

            ProjectItems(Employees, batch);

            for (int i = 0; i < days; i++)
            {
                Console.WriteLine($"--------------DAY-{i}--------------");
                var cfhEmps = qUtil.Next(batch) as List<Employee>;
                //PrintEmployees(cfhEmps);

                var inOfficeEmps = emps.Except(cfhEmps).ToList();
                //PrintEmployees(inOfficeEmps);

                Allocate(seats, inOfficeEmps, prevcfh, cfhEmps);
                //prevcfh = GetNextCfh(cfhEmps, emps, batch);
                prevcfh = cfhEmps;
            }

            Console.WriteLine("Hello World!");
        }

        private static void ProjectItems<T>(List<T> emps, int batch)
        {
            var que = QueueUtil<T>.InitQueue(emps);
            var qUtil = new QueueUtil<T>(que);

            //var cfhMap2.Add(cfh, GetNext<Employee>(cfhEmps2, c)); //= cfhEmps2.Select(c => new { cfh = c, nxt = GetNext<Employee>(cfhEmps2, c)});

            var prevcfh = new List<T>();
            var allCfhPairs = new List<List<T>>();
            for (int i = 0; i < days; i++)
            {
                allCfhPairs.Add(qUtil.Next(batch) as List<T>);
            }

            var projectedCfh = new List<(List<T>, List<T>, List<T>)>();
            foreach (var item in allCfhPairs)
            {
                var PCN = (GetPrevious(allCfhPairs, item), item, GetNext(allCfhPairs, item));
                projectedCfh.Add(PCN);
            }

            

            foreach (var item in projectedCfh)
            {
                
                PrintCollection<T>(item.Item1);
                PrintCollection<T>(item.Item2);
                PrintCollection<T>(item.Item3);
                Console.WriteLine();
            }
        }

        private static List<Employee> GetNextCfh(List<Employee> cfhEmps, List<Employee> emps, int batch)
        {
            var cfhNext2 = new List<Employee>();
            foreach (var item in cfhEmps)
            {
                var countIndex = emps.Count() - 1;
                if ((item.Id + batch)>= countIndex)
                {
                    cfhNext2.Add(emps[countIndex - (int)item.Id - 1]);
                }
                else
                {
                    cfhNext2.Add(emps[(int)item.Id + batch]);
                }
            }
            return cfhNext2;
        }



        private static void Allocate(List<Seat> seats, List<Employee> inOfficeEmps, List<Employee> prevCfh, List<Employee> currentCfh)
        {
            if (!prevCfh.Any())
            {
                for (int i = 0; i < inOfficeEmps.Count(); i++)
                {
                    inOfficeEmps[i].seat = seats[i];
                }            
            }
            else
            {
                //replace seat of prev cfh with current cfh
                for (int i = 0; i < currentCfh.Count(); i++)
                {
                    inOfficeEmps.Find(o => o.Id == prevCfh[i].Id).seat = currentCfh[i].seat;
                }
            }
            
            inOfficeEmps.ForEach(o=> Console.WriteLine(o.ToString()));
        }

        private static void PrintEmployees(List<Employee> cfhEmps)
        {
            var empStr = "";
            foreach (var item in cfhEmps)
            {
                empStr = empStr + item.Name + " , ";
            }
            Console.WriteLine(empStr);
        }

        private static void PrintCollection<T>(List<T> cfhEmps)
        {
            if (cfhEmps!=null)
            {
                var empStr = "";
                foreach (var item in cfhEmps)
                {
                    empStr = empStr + item.ToString() + " , ";
                }
                Console.Write(empStr + "==>");
            }
            Console.Write("NULL" + "==>");
        }

        private static T GetNext<T>(IEnumerable<T> list, T current)
        {
            try
            {
                return list.SkipWhile(x => !x.Equals(current)).Skip(1).First();
            }
            catch
            {
                return default(T);
            }
        }

        private static T GetPrevious<T>(IEnumerable<T> list, T current)
        {
            try
            {
                return list.TakeWhile(x => !x.Equals(current)).Last();
            }
            catch
            {
                return default(T);
            }
        }
    }
}
