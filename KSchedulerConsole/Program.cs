﻿using System;
using System.Collections.Generic;
using System.Collections;
using Scheduler;
using System.Linq;

namespace KSchedulerConsole
{
    class Program
    {
        public static IReadOnlyList<string> Employees = new List<string> { "A", "B", "C","D", "E", "F", "G", "H"};
        private const int days = 5;

        static void Main(string[] args)
        {
            
            var (seats, emps) = RegistrationUtil.Seed(10, 7);

            int batch = 3;

            var que = QueueUtil<Employee>.InitQueue(emps);
            var qUtil = new QueueUtil<Employee>(que);
            
            var prevcfh = new List<Employee>();
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

            Console.ReadLine();
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
                    inOfficeEmps[inOfficeEmps.FindIndex(o => o.Id == prevCfh[i].Id)].seat = currentCfh[i].seat;
                }
            }
            
            inOfficeEmps.OrderBy(o=>o.seat.Name).ToList().ForEach(o=> Console.WriteLine(o.ToString()));
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
    }
}
