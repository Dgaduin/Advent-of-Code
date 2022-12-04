using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_04;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");
        var pairs = input.ReadPairs();

        Console.WriteLine(Task1(pairs));
        Console.WriteLine(Task2(pairs));
    }

    public static int Task1(List<List<int>> pairs) =>
        pairs.Count(x =>
            (x[0] <= x[2] && x[1] >= x[3]) ||
            (x[0] >= x[2] && x[1] <= x[3]));

    public static int Task2(List<List<int>> pairs) =>
        pairs.Count(x =>
            (x[1] >= x[2] && x[1] <= x[3]) ||
            (x[3] >= x[0] && x[3] <= x[1]));

    public static List<List<int>> ReadPairs(this List<string> input) =>
            input
                .Select(row => row.Split(',', '-')
                                  .Select(Int32.Parse)
                                  .ToList())
                .ToList();
}
