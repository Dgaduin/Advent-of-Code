using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();

            Console.WriteLine(input.Select(x => Task1(Int32.Parse(x))).Sum());
            Console.WriteLine(input.Select(x => Task2(Int32.Parse(x))).Sum());
        }

        public static int Task1(int value) => value / 3 - 2;

        public static int Task2(int value, int fuelTotal = 0)
        {
            var fuel = Task1(value);
            if (fuel <= 8) return fuel + fuelTotal;
            else return Task2(fuel, fuel + fuelTotal);
        }
    }
}
