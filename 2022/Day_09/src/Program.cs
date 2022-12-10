using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_09;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2());
    }

    public static int Task1(List<string> input)
    {
        var set = new HashSet<int>();
        var tail = (x: 0, y: 0);
        set.UpdateSet(tail);
        var head = (x: 0, y: 0);
        var movements = new List<(int x, int y)>();
        foreach (var row in input)
        {
            var split = row.Split(' ');
            var command = split[0];
            var count = Int32.Parse(split[1]);
            for (int i = 0; i < count; i++)
            {
                var move = command switch
                {
                    "U" => (x: 1, y: 0),
                    "D" => (x: -1, y: 0),
                    "L" => (x: 0, y: -1),
                    "R" => (x: 0, y: 1),
                };
                movements.Add(move);
            }
        }

        for (int i = 0; i < movements.Count; i++)
        {
            var cur = movements[i];
            head.ApplyMove(cur);
            var tailMove = GetDirectionDistance(head, tail);
            tail.ApplyMove(tailMove);
            set.UpdateSet(tail);
        }

        return set.Count;

    }
    public static string Task2() { return ""; }

    public static void UpdateSet(this HashSet<int> hSet, (int, int) point) => hSet.Add(HashCode.Combine(point.Item1, point.Item2));
    public static void ApplyMove(this ref (int, int) point, (int, int) move)
    {
        point.Item1 += move.Item1;
        point.Item2 += move.Item2;
    }
    public static (int, int) GetDirectionDistance(this (int, int) head, (int, int) tail)
    {
        var x = head.Item1 - tail.Item1;
        var y = head.Item2 - tail.Item2;
        var retVal = (0, 0);

        if (Math.Abs(x) > 1) y = y * 2;
        if (Math.Abs(y) > 1) x = x * 2;

        if (x > 1)
            retVal.Item1 = 1;
        if (x < -1)
            retVal.Item1 = -1;
        if (y > 1)
            retVal.Item2 = 1;
        if (y < -1)
            retVal.Item2 = -1;

        return retVal;
    }
}
