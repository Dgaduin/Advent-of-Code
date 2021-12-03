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
            var input = File.ReadLines("input.txt").Select(SplitParam).ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<(string, int)> collection)
        {
            int d = 0, f = 0;
            foreach (var item in collection)
            {
                switch (item.Item1)
                {
                    case "forward":
                        f += item.Item2;
                        break;
                    case "up":
                        d -= item.Item2;
                        break;
                    case "down":
                        d += item.Item2;
                        break;
                }
            }
            return d * f;
        }
        public static int Task2(List<(string, int)> collection)
        {
            int d = 0, f = 0, a = 0;
            foreach (var item in collection)
            {
                switch (item.Item1)
                {
                    case "forward":
                        f += item.Item2;
                        d += a * item.Item2;
                        break;
                    case "up":
                        a -= item.Item2;
                        break;
                    case "down":
                        a += item.Item2;
                        break;
                }
            }
            return d * f;
        }

        public static (string, int) SplitParam(string param)
        {
            string[] split = param.Split(' ');
            return (split[0], int.Parse(split[1]));
        }
    }
}
