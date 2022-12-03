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
        var task1Commons = input.Task1Commons().ToList();
        var task2Backpacks = input.Select(x => x.ToHashSet()).ToList();

        Console.WriteLine(Task1(task1Commons));
        Console.WriteLine(Task2(task2Backpacks));
    }

    public static int Task1(List<char> common) => common.Sum(x => x.CalculateScore());

    public static int Task2(List<HashSet<char>> backpacks)
    {
        int score = 0;
        for (int i = 0; i < backpacks.Count; i += 3)
        {
            var common = backpacks[i].Intersect(
                            backpacks[i + 1].Intersect(
                                backpacks[i + 2]))
                         .FirstOrDefault();

            score += common.CalculateScore();
        }
        return score;
    }

    public static IEnumerable<char> Task1Commons(this List<string> input)
    {
        foreach (var row in input)
        {
            int half = row.Length / 2;
            var firstHalf = row.Take(half).ToHashSet();
            var secondHalf = row.Skip(half).ToHashSet();
            var common = firstHalf.Intersect(secondHalf).FirstOrDefault();
            yield return common;
        }
        yield break;
    }

    public static int CalculateScore(this char a) => a - 'a' < 0 ? a - 'A' + 27 : a - 'a' + 1;
}
