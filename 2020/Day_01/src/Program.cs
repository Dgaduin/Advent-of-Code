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
            List<int> input = File.ReadLines("input.txt").Select(x => Int32.Parse(x)).ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<int> input)
        {
            for (int i = 0; i < input.Count - 1; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    var a = input[i];
                    var b = input[j];
                    if (a + b == 2020) return a * b;
                }
            }
            return 0;
        }

        public static int Task2(List<int> input)
        {
            for (int i = 0; i < input.Count - 2; i++)
            {
                for (int j = i + 1; j < input.Count - 1; j++)
                {
                    for (int k = j + 1; k < input.Count; k++)
                    {
                        var a = input[i];
                        var b = input[j];
                        var c = input[k];
                        if (a + b + c == 2020) return a * b * c;
                    }
                }
            }
            return 0;
        }
    }
}
