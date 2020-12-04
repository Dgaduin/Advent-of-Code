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
            List<List<char>> input = File.ReadLines("input.txt").Select(x => x.ToList()).ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<List<char>> input)
        {
            return NavigateSlope(input, 3, 1);
        }

        public static int Task2(List<List<char>> input)
        {
            var tc1 = NavigateSlope(input, 1, 1);
            var tc2 = NavigateSlope(input, 3, 1);
            var tc3 = NavigateSlope(input, 5, 1);
            var tc4 = NavigateSlope(input, 7, 1);
            var tc5 = NavigateSlope(input, 1, 2);
            return tc1 * tc2 * tc3 * tc4 * tc5;
        }

        public static int NavigateSlope(List<List<char>> input, int xSlope, int ySlope)
        {
            int x = 0, y = 0, xLimit = input[0].Count, treeCount = 0;
            while (y < input.Count - ySlope)
            {
                x += xSlope;
                y += ySlope;
                if (input[y][x % xLimit] == '#')
                    treeCount++;
            }
            return treeCount;
        }
    }
}
