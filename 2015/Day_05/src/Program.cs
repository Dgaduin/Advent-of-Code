using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_05
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

        public static string Task1(List<string> input) => input.Where(x => IsNice1(x)).Count().ToString();
        public static string Task2(List<string> input) => input.Where(x => IsNice2(x)).Count().ToString();
        public static bool IsNice1(string input)
        {
            if (input.Contains("ab") || input.Contains("cd") || input.Contains("pq") || input.Contains("xy"))
                return false;

            var sum = input.Count(x => x == 'a') + input.Count(x => x == 'e') + input.Count(x => x == 'i') + input.Count(x => x == 'o') + input.Count(x => x == 'u');
            if (sum < 3)
                return false;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsNice2(string input)
        {
            var r1 = new Regex("([a-z][a-z]).*\\1");
            var r2 = new Regex("([a-z])[a-z]\\1");

            if (r1.IsMatch(input) && r2.IsMatch(input))
            {
                return true;
            }

            return false;
        }
    }
}
