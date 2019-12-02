using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            // List<string> input = File.ReadLines("input.txt").ToList();
            string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(string input)
        {
            int up = input.Count(x => x == '(');
            int down = input.Count(x => x == ')');
            return (up - down).ToString();
        }
        public static string Task2(string input)
        {
            int first = 0;
            for (int i = 0, level = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                    level++;
                else level--;
                if (level < 0)
                {
                    first = i + 1;
                    break;
                }
            }
            return first.ToString();
        }
    }
}
