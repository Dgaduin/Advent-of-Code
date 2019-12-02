using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03
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
            HashSet<string> cords = new HashSet<string>();
            cords.Add("0 0");
            int x = 0, y = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '>':
                        x++;
                        break;
                    case '<':
                        x--;
                        break;
                    case '^':
                        y++;
                        break;
                    case 'v':
                        y--;
                        break;
                }
                cords.Add($"{x} {y}");
            }
            return cords.Count.ToString();
        }
        public static string Task2(string input)
        {
            HashSet<string> cords = new HashSet<string>();
            cords.Add("0 0");
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            for (int i = 0; i < input.Length; i += 2)
            {
                switch (input[i])
                {
                    case '>':
                        x1++;
                        break;
                    case '<':
                        x1--;
                        break;
                    case '^':
                        y1++;
                        break;
                    case 'v':
                        y1--;
                        break;
                }
                switch (input[i + 1])
                {
                    case '>':
                        x2++;
                        break;
                    case '<':
                        x2--;
                        break;
                    case '^':
                        y2++;
                        break;
                    case 'v':
                        y2--;
                        break;
                }
                cords.Add($"{x1} {y1}");
                cords.Add($"{x2} {y2}");
            }
            return cords.Count.ToString();
        }
    }
}
