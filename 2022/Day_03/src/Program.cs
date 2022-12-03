using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");
        var backpacks = input.ProcessBackpacks();

        Console.WriteLine(Task1(backpacks));
        Console.WriteLine(Task2());
    }

    public static int Task1(IEnumerable<(HashSet<char>, HashSet<char>, char common)> backpacks) => backpacks.Sum(x => x.common.CalculateScore());

    public static string Task2() { return ""; }

    public static IEnumerable<(HashSet<char>, HashSet<char>, char)> ProcessBackpacks(this List<string> input)
    {
        foreach (var row in input)
        {
            int half = row.Length / 2;
            var firstHalf = row.Take(half).ToHashSet();
            var secondHalf = row.Skip(half).ToHashSet();
            var common = firstHalf.Intersect(secondHalf).FirstOrDefault();
            yield return (firstHalf, secondHalf, common);
        }
        yield break;
    }

    public static int CalculateScore(this char a) => a - 'a' < 0 ? a - 'A' + 27 : a - 'a' + 1;
}
