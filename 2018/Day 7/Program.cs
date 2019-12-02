using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day_7
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<string> input = File.ReadAllLines("input.txt");
            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }
        public static string Task1(IEnumerable<string> input)
        {
            var points = input.Select(x =>
            {
                var temp = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return (temp[1], temp[7]);
            }).ToList();

            var ends = new HashSet<string>(points.Select(x => x.Item2));
            var options = new SortedSet<string>(points.Where(x => !ends.Contains(x.Item1)).Select(x => x.Item1));
            var output = new StringBuilder();
            var start = options.First();

            while (options.Count() > 0)
            {
                output.Append(start);

                var newOptions = points.Where(x => x.Item1 == start).ToList();
                points.RemoveAll(x => x.Item1 == start);
                options.Remove(start);
                foreach (var o in newOptions)
                {
                    if (!points.Any(x => x.Item2 == o.Item2))
                        options.Add(o.Item2);

                }
                if (options.Count > 0)
                    start = options.First();
            }
            return output.ToString();
        }

        public static string Task2(IEnumerable<string> input)
        {
            // Fetch the items
            var points = input.Select(x =>
            {
                var temp = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return Tuple.Create(temp[1], temp[7]);
            }).ToList();

            // All end options
            var ends = new HashSet<string>(points.Select(x => x.Item2));
            // All options where there are no ends that match their start, sorted
            var options = new SortedSet<string>(points.Where(x => !ends.Contains(x.Item1)).Select(x => x.Item1));
            // First is alphabetically first
            var start = options.First();
            // Timer
            int currentIteration = 0;
            // "Threads"
            List<(string Node, int Time)> timeShare = new List<(string Node, int time)> {
                ("",0),
                ("",0),
                ("",0),
                ("",0),
                ("",0)
            };

            // While there are options
            while (options.Count() > 0)
            {
                // All possible options that have the same starting node as the currently selected
                var newOptions = points.Where(x => x.Item1 == start).ToList();
                // Remove all nodes where the start is the same as the currently selected
                points.RemoveAll(x => x.Item1 == start);
                // Remove the selected node from the options list
                options.Remove(start);

                foreach (var o in newOptions)
                {
                    // If there are no other node that depend on it - add it to the possible options
                    if (!points.Any(x => x.Item2 == o.Item2))
                        options.Add(o.Item2);
                }

                var t = timeShare.Min(x => x.Time);
                t.

                // If there are options - starting node is the first alphabetically 
                if (options.Count > 0)
                {
                    start = options.First();
                }
            }
            return "";
        }
    }
}
