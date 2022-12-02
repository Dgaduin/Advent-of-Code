using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_02;
public class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").ToList();
        var strategy = GetShapes(input);
        // string input = File.ReadAllText("input.txt");

        Console.WriteLine(Task1(strategy));
        Console.WriteLine(Task2());
    }

    public static int Task1(List<(int, int)> input) { return 0; }
    public static string Task2() { return ""; }

    public static List<(int, int)> GetShapes(List<string> input)
    =>
        input.Select(x =>
        {
            var y = x.Split(' ');
            return (Int32.Parse(y[0]), Int32.Parse(y[2]));
        }
        ).ToList();
}
