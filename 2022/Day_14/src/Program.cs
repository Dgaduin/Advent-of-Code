using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_14;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");
        var map = CreateMap(input);
        Task1(map);
    }

    public static void Task1(HashSet<(int x, int y)> map)
    {
        var lowerBound = map.MaxBy(a => a.y).y;

        var floor = Enumerable.Range(0, 1000).Select(x => (x, lowerBound + 2));
        map.UnionWith(floor);

        var startingPoint = (x: 500, y: 0);
        var sand = startingPoint;
        var counter = 0;
        var t1Flag = true;

        while (true)
        {
            if (t1Flag && sand.y >= lowerBound)
            {
                t1Flag = false;
                Console.WriteLine(counter);
            }
            if (map.Contains(startingPoint))
            {
                Console.WriteLine(counter);
                break;
            }

            if (!map.Contains((sand.x, sand.y + 1)))
                sand = (sand.x, sand.y + 1);
            else if (!map.Contains((sand.x - 1, sand.y + 1)))
                sand = (sand.x - 1, sand.y + 1);
            else if (!map.Contains((sand.x + 1, sand.y + 1)))
                sand = (sand.x + 1, sand.y + 1);
            else
            {
                map.Add(sand);
                counter++;
                sand = startingPoint;
            }
        }
    }

    public static HashSet<(int x, int y)> CreateMap(List<string> input)
    {
        var map = new HashSet<(int x, int y)>();

        var parsedInput = input.Select(x =>
            x.Split(" -> ")
             .Select(y =>
             {
                 var t = y.Split(',');
                 return (x: Int32.Parse(t[0]), y: Int32.Parse(t[1]));
             })
             .ToList());

        foreach (var line in parsedInput)
        {
            var previousPoint = line[0];
            for (int i = 1; i < line.Count; i++)
            {
                var nextPoint = line[i];
                if (nextPoint.x == previousPoint.x)
                {
                    var min = Math.Min(previousPoint.y, nextPoint.y);
                    var max = Math.Max(previousPoint.y, nextPoint.y);
                    var t = Enumerable.Range(min, max - min).Select(x => (x: previousPoint.x, y: x));
                    map.UnionWith(t);
                }
                else
                {
                    var min = Math.Min(previousPoint.x, nextPoint.x);
                    var max = Math.Max(previousPoint.x, nextPoint.x);
                    var t = Enumerable.Range(min, max - min + 1).Select(x => (x: x, y: previousPoint.y));
                    map.UnionWith(t);
                }
                previousPoint = nextPoint;
            }
        }

        return map;
    }
}
