using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_02
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

        public static string Task1(List<string> input)
        {
            var dimensions = input
                .Select(x =>
                    x.Split('x', System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => Int32.Parse(y))
                    .ToList()
                    );

            return dimensions.Select(y =>
                {
                    List<int> l = new List<int>();
                    l.Add(y[0] * y[1]);
                    l.Add(y[1] * y[2]);
                    l.Add(y[2] * y[0]);
                    return l.Sum(z => 2 * z) + l.Min();
                })
                .Sum()
                .ToString();
        }
        public static string Task2(List<string> input)
        {
            var dimensions = input
                .Select(x =>
                    x.Split('x', System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => Int32.Parse(y))
                    .ToList()
                    );

            return dimensions
                .Select(y =>
                    {
                        List<int> l = new List<int>();
                        l.Add(2 * y[0] + 2 * y[1]);
                        l.Add(2 * y[1] + 2 * y[2]);
                        l.Add(2 * y[2] + 2 * y[0]);
                        return l.Min() + y[0] * y[1] * y[2];
                    })
                .Sum()
                .ToString();
        }
    }
}
