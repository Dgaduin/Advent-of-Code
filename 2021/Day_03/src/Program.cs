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
            var input = File.ReadLines("input.txt").Select(ParseInput).ToList();

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(List<int[]> input, int size = 12)
        {
            int[] sums = new int[size];

            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < size; j++)
                    sums[j] += input[i][j];

            int[] results1 = new int[size];
            int[] results2 = new int[size];

            for (int j = 0; j < size; j++)
            {
                results1[j] = input.Count / 2 - sums[j] > 0 ? 1 : 0;
                results2[j] = input.Count / 2 - sums[j] > 0 ? 0 : 1;
            }

            var result1 = Convert.ToInt32(string.Join("", results1), 2);
            var result2 = Convert.ToInt32(string.Join("", results2), 2);

            return $"{result1 * result2}";
        }

        public static string Task2(List<int[]> input, int size = 12)
        {
            var inputCopy = input;
            int[] results1 = new int[size];
            for (int j = 0; j < size; j++)
            {
                var temp = inputCopy.Sum(x => x[j]);
                var compare = inputCopy.Count % 2 + inputCopy.Count / 2 - temp > 0 ? 0 : 1;
                inputCopy = inputCopy.Where(x => x[j] == compare).ToList();
                if (inputCopy.Count == 1)
                {
                    results1 = inputCopy[0];
                    break;
                }
            }

            inputCopy = input;
            int[] results2 = new int[size];
            for (int j = 0; j < size; j++)
            {
                var temp = inputCopy.Sum(x => x[j]);
                var compare = inputCopy.Count % 2 + inputCopy.Count / 2 - temp > 0 ? 1 : 0;
                inputCopy = inputCopy.Where(x => x[j] == compare).ToList();
                if (inputCopy.Count == 1)
                {
                    results2 = inputCopy[0];
                    break;
                }
            }
            var result1 = Convert.ToInt32(string.Join("", results1), 2);
            var result2 = Convert.ToInt32(string.Join("", results2), 2);
            return $"{result1 * result2}";
        }
        public static int[] ParseInput(string input) => input.Select(x => int.Parse(x.ToString())).ToArray();

    }
}
