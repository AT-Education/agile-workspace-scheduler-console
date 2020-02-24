using System;
namespace KSchedulerConsole
{
    public class Seat
    {
        public Seat()
        { }

        public long Id
        {
            get;
            set;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id} ~ {Name}";
        }
    }
}
