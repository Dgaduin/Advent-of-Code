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
            List<int> input = File.ReadLines("input.txt").Select(Int32.Parse).ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<int> input)
        {
            int bigger = 0;
            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] > input[i - 1])
                {
                    bigger++;
                }
            }
            return bigger;
        }
        public static int Task2(List<int> input)
        {
            int bigger = -1, last = 0;
            for (int i = 0; i < input.Count - 2; i++)
            {
                var sum = input[i] + input[i + 1] + input[i + 2];
                if (sum > last)
                {
                    bigger++;
                }
                last = sum;
            }
            return bigger;
        }
    }
}
