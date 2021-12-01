using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_09
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<long> input = File.ReadLines("input.txt").Select(x => long.Parse(x)).ToList();
            // string input = File.ReadAllText("input.txt");
            var t1 = Task1(input);
            Console.WriteLine(t1);
            Console.WriteLine(Task2(input, t1));
        }

        public static long Task1(List<long> input, int range = 25)
        {
            for (int i = range; i < input.Count; i++)
            {
                int t = -1;
                for (int j = i - range; j < i; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (input[k] + input[j] == input[i]) t = i;
                    }
                }
                if (t == -1) return input[i];
            }
            return 0;
        }
        public static long Task2(List<long> input, long t1)
        {
            for (int i = 0; i < input.Count; i++)
            {
                long sum = 0;
                int j = i;
                long min = long.MaxValue;
                long max = long.MinValue;
                while (sum < t1)
                {
                    sum += input[j];
                    if (input[j] < min) min = input[j];
                    if (input[j] > max) max = input[j];
                    j++;
                }
                if (sum == t1) return min + max;
            }
            return 0;
        }
    }
}
