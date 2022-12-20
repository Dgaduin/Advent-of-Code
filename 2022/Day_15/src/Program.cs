using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_15;
public static class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").ParseInput();
        // string input = File.ReadAllText("input.txt");

        Task1(input);
    }

    public static void Task1(List<(long, long, long, long)> input)
    {
        var target = 4000000;
        var sensors = input
                    .Select(a => (x: a.Item1, y: a.Item2, r: MDistance(a)))
                    .ToList();
        var mapStack = GenerateRowMatches(target, sensors);

        Console.WriteLine(mapStack.Aggregate(new long(), (a, b) => a += (b.Item2 - b.Item1) + 1) - 1);

        for (long i = 0; i <= 4000000; i++)
        {
            mapStack = GenerateRowMatches(i, sensors);
            if (mapStack.Count > 1)
            {
                Console.Write($"{i}:");
                mapStack.ForEach(x => Console.Write($"{x} "));
                var value = (mapStack[0].Item2 + 1) * 4000000 + i;
                Console.WriteLine(value);
            }
        }

        long MDistance((long ax, long ay, long bx, long by) z) => Math.Abs(z.ax - z.bx) + Math.Abs(z.ay - z.by);
    }

    private static List<(long, long)> GenerateRowMatches(long target, List<(long x, long y, long r)> sensors)
    {
        var map = new List<(long, long)>();

        foreach (var sensor in sensors)
        {
            var targetOffset = Math.Abs(target - sensor.y);
            var distance = sensor.r - targetOffset;
            if (targetOffset <= sensor.r)
                map.Add((sensor.x - distance, sensor.x + distance));
        }

        var orderedMap = map.OrderBy(x => x.Item1).ToList();
        var mapStack = new List<(long, long)>();
        var prevMap = orderedMap[0];
        for (int i = 1; i < orderedMap.Count; i++)
        {
            var curMap = orderedMap[i];
            if (curMap.Item1 >= prevMap.Item1 && curMap.Item1 <= prevMap.Item2 + 1)
            {
                if (curMap.Item2 >= prevMap.Item2 || curMap.Item1 == prevMap.Item2 + 1)
                    prevMap.Item2 = curMap.Item2;
            }
            else
            {
                mapStack.Add(prevMap);
                prevMap = curMap;
            }
        }
        mapStack.Add(prevMap);
        return mapStack;
    }

    public static List<(long, long, long, long)> ParseInput(this IEnumerable<string> input)
    {
        var regex = new Regex("Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)");
        return input.Select(x =>
                       {
                           var match = regex.Match(x);
                           return (long.Parse(match.Groups[1].Value),
                                   long.Parse(match.Groups[2].Value),
                                   long.Parse(match.Groups[3].Value),
                                   long.Parse(match.Groups[4].Value));
                       }).ToList();
    }
}