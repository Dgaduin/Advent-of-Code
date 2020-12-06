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

        public static int Task1(List<string> input) => input.Select(GetId).Max();
        public static string Task2(List<string> input)
        {
            var seats = input.Select(GetId).OrderBy(x => x).ToList();
            for (int i = 1; i < seats.Count; i++)
            {
                if (Math.Abs(seats[i] - seats[i - 1]) != 1)
                    return $"{seats[i]} {seats[i - 1]}";
            }
            return "Fail";
        }

        // Because we have the x8 offset we can 
        // parse the whole thing in straight binary
        public static int GetId(string input) =>
            Convert.ToInt32(
                input.Replace("B", "1")
                     .Replace("F", "0")
                     .Replace("R", "1")
                     .Replace("L", "0"), 2);

        public static int GetIdOld(string input)
        {
            var column = BinaryPartition(input[7..], 'L', 0, 7);
            var row = BinaryPartition(input.Substring(0, 7), 'F', 0, 127);
            return row * 8 + column;
        }

        public static int BinaryPartition(string input, char lowerLimit, int min, int max)
        {
            if (input.Length == 1)
            {
                if (input[0] == lowerLimit)
                    return min;
                else return max;
            }
            else
            {
                int halfPoint = (max - min) / 2;
                if ((max - min) % 2 == 1) halfPoint++;
                if (input[0] == lowerLimit)
                    max -= halfPoint;
                else
                    min += halfPoint;
                return BinaryPartition(input[1..], lowerLimit, min, max);
            }
        }
    }
}
