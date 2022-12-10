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

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2(input));
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
    public static int Task2(List<string> input)
    {
        var maxScore = 0;
        var rowLength = input.First().Length;

        for (int i = 1; i < input.Count - 1; i++)
        {
            for (int j = 1; j < rowLength - 1; j++)
            {
                var score = GetScore(input, i, j);
                if (score > maxScore) maxScore = score;
            }
        }
        return maxScore;

        int GetScore(List<string> input, int x, int y)
        {
            int[] maxes = new int[] { 0, 0, 0, 0 }; // right,bottom,left,top
            var rowLength = input.First().Length;
            var tree = input[x][y];
            for (int i = y + 1; i < rowLength; i++)
            {
                maxes[0]++;
                if (tree <= input[x][i]) break;
            }
            for (int i = x + 1; i < input.Count; i++)
            {
                maxes[1]++;
                if (tree <= input[i][y]) break;
            }
            for (int i = y - 1; i >= 0; i--)
            {
                maxes[2]++;
                if (tree <= input[x][i]) break;
            }
            for (int i = x - 1; i >= 0; i--)
            {
                maxes[3]++;
                if (tree <= input[i][y]) break;
            }
            return maxes[0] * maxes[1] * maxes[2] * maxes[3];
        }
    }
}
