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

        public static int Task1(List<string> input) =>
            input.Select(x => ParseInput(x))
                 .Count(x => x.IsValidTask1());
        public static int Task2(List<string> input) =>
            input.Select(x => ParseInput(x))
                 .Count(x => x.IsValidTask2());

        private static Regex ParsingRegex = new Regex("(\\d+)-(\\d+) (\\w): (\\w+)");
        private static PasswordRule ParseInput(string input)
        {
            var match = ParsingRegex.Match(input).Groups;
            return new PasswordRule
            {
                Min = Int32.Parse(match[1].Value),
                Max = Int32.Parse(match[2].Value),
                Character = match[3].Value[0],
                Password = match[4].Value
            };
        }

        private class PasswordRule
        {
            public char Character { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public string Password { get; set; }

            public bool IsValidTask1()
            {
                var count = Password.Where(x => x == Character).Count();
                return count >= Min && count <= Max;
            }

            public bool IsValidTask2() => Character == Password[Min - 1] ^ Character == Password[Max - 1];
        }
    }
}
