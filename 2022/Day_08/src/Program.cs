using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_08;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2());
    }

    public static int Task1(List<string> input)
    {
        int count = 2 * input.Count + 2 * input.First().Length - 4;
        var topMaxes = input.First().ToArray();
        var bottomMaxes = input.Last().ToArray();
        var leftMaxes = input.Select(x => x[0]).ToArray();
        var rightMaxes = input.Select(x => x[^1]).ToArray();
        var tempSet = new HashSet<int>();

        for (int i = 1; i < input.Count - 1; i++)
        {
            var row = input[i];
            var inverseI = input.Count - (i + 1);
            var inverseRow = input[inverseI];

            for (int j = 1; j < row.Length - 1; j++)
            {
                var tree = row[j];
                if (tree > leftMaxes[i])
                {
                    tempSet.Add(HashCode.Combine(i, j));
                    leftMaxes[i] = tree;
                }
                if (tree > topMaxes[j])
                {
                    tempSet.Add(HashCode.Combine(i, j));
                    topMaxes[j] = tree;
                }

                var inverseJ = row.Length - (j + 1);
                var inverseTree = inverseRow[inverseJ];
                if (inverseTree > rightMaxes[inverseI])
                {
                    tempSet.Add(HashCode.Combine(inverseI, inverseJ));
                    rightMaxes[inverseI] = inverseTree;
                }
                if (inverseTree > bottomMaxes[inverseJ])
                {
                    tempSet.Add(HashCode.Combine(inverseI, inverseJ));
                    bottomMaxes[inverseJ] = inverseTree;
                }
            }
        }
        count += tempSet.Count;
        return count;
    }
    public static string Task2() { return ""; }
}
