using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_06
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<string> input)
        {
            var list = new List<string>();
            int sum = 0;
            foreach (var t in input)
            {
                if (String.IsNullOrWhiteSpace(t))
                {
                    sum += CountGroupAnyYes(list);
                    list = new List<string>();
                }
                else
                    list.Add(t);
            }
            sum += CountGroupAnyYes(list);
            return sum;
        }
        public static int Task2(List<string> input)
        {
            var list = new List<string>();
            int sum = 0;
            foreach (var t in input)
            {
                if (String.IsNullOrWhiteSpace(t))
                {
                    sum += CountGroupAllYes(list);
                    list = new List<string>();
                }
                else
                    list.Add(t);
            }
            sum += CountGroupAllYes(list);
            return sum;
        }

        public static int CountGroupAnyYes(List<string> input) => input.SelectMany(x => x).ToHashSet().Count;
        public static int CountGroupAllYes(List<string> input) => input.Select(x => x.ToHashSet())
                                                                       .Aggregate((a, b) => a.Intersect(b)
                                                                                             .ToHashSet()).Count;
    }
}
