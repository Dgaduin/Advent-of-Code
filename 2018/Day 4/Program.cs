using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();
            input.Sort();
            var events = input.Select(x => new PatrolEvent(x)).ToList();
            for (int i = 1; i < events.Count(); i++)
            {
                if (events[i].id == -1)
                    events[i].id = events[i - 1].id;
            }
            var byGuard = events.GroupBy(x => x.id);
            int maxMinutes = 0;
            int returnValueTask1 = 0;

            int maxMinute = 0;
            int returnValueTask2 = 0;
            foreach (var guard in byGuard)
            {
                var dict = Enumerable.Range(0, 60).ToDictionary(x => x, x => 0);
                int prevMin = 0;
                foreach (var evenT in guard)
                {
                    if (evenT.eventType == 1)
                        prevMin = 0;
                    if (evenT.eventType == 2)
                        prevMin = evenT.timestamp.Minute;
                    if (evenT.eventType == 3)
                    {
                        for (int i = prevMin; i < evenT.timestamp.Minute; i++)
                        {
                            dict[i]++;
                        }
                        prevMin = 0;
                    }

                }
                int minutes = dict.Sum(x => x.Value);
                var maxMinsInDay = dict.Aggregate((i, j) => i.Value > j.Value ? i : j);
                if (minutes > maxMinutes)
                {
                    maxMinutes = minutes;

                    returnValueTask1 = guard.Key * maxMinsInDay.Key;
                }

                if (maxMinsInDay.Value > maxMinute)
                {
                    maxMinute = maxMinsInDay.Value;
                    returnValueTask2 = guard.Key * maxMinsInDay.Key;
                }

            }
            Console.WriteLine(returnValueTask1);
            Console.WriteLine(returnValueTask2);
        }
    }

    public class PatrolEvent
    {
        public DateTime timestamp { get; set; }
        public int id { get; set; } = -1;
        public int eventType { get; set; }

        public PatrolEvent(string s)
        {
            timestamp = timestamp = DateTime.Parse(s.Substring(1, 16));
            var temp = s.Substring(18).Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToList();
            if (temp[0][0] == 'G')
            {
                id = Int32.Parse(temp[1].Substring(1));
                eventType = 1;
            }
            if (temp[0][0] == 'f')
            {
                eventType = 2;
            }
            if (temp[0][0] == 'w')
            {
                eventType = 3;
            }
        }
    }
}
