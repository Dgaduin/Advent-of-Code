using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Day_12;
public class Program
{
    public static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");
        var best = new Dictionary<(int, int), int>();
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine(Task1(input, best));
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        sw = Stopwatch.StartNew();
        Console.WriteLine(Task2(input, best));
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    public static int Task1(List<string> input, Dictionary<(int, int), int> best)
    {
        var start = GetStart(input);
        var result = Move(input, start, new HashSet<(int, int)>(), best);
        return result;
    }

    public static int Task2(List<string> input, Dictionary<(int, int), int> best)
    {
        var start = GetStart2(input);
        var result = start.Select(x => Move(input, x, new HashSet<(int, int)>(), best)).Min();
        return result;
    }

    public static int Move(List<string> input, (int i, int j) a, HashSet<(int, int)> set, Dictionary<(int, int), int> best)
    {
        //var set = new List<(int, int)>(previousPath);
        set.Add((a));

        if (best.TryGetValue(a, out int value))
            if (value <= set.Count)
            {
                // Console.WriteLine("Better path found");
                set.Remove((a));
                return Int32.MaxValue;
            }

        best[a] = set.Count;

        var neighbours = GetUnvisitedNeighbours(input, a.i, a.j, set);

        if (!neighbours.Any())
        {
            // Console.WriteLine("No neighbours");
            set.Remove((a));
            return Int32.MaxValue;
        }

        var rank = new List<int>();

        foreach (var square in neighbours)
        {
            var field = input[square.x][square.y];
            if (field == 'E')
            {
                set.Remove((a));
                return set.Count;
            }
            rank.Add(Move(input, square, set, best));
        }

        // foreach (var x in set)
        //     Console.Write($"({x.Item1}-{x.Item2}) ");
        // Console.WriteLine();

        // Console.WriteLine("Returning normal end");
        set.Remove((a));
        return rank.Min();
    }

    public static (int i, int j) GetStart(List<string> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            var j = input[i].IndexOf('S');
            if (j != -1)
                return (i, j);
        }
        return (0, 0);
    }

    public static IEnumerable<(int i, int j)> GetStart2(List<string> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'a' || input[i][j] == 'S')
                    yield return (i, j);
            }
        }
        yield break;
    }

    public static IEnumerable<(int x, int y)> GetUnvisitedNeighbours(List<string> input, int i, int j, HashSet<(int, int)> previousPath)
    {
        var map = new List<(int x, int y)>
        {
            (0,-1),(1,0),(0,1),(-1,0),
        };

        var neighbours = map
            .Select(a => (x: i + a.x, y: j + a.y))
            .Where(a => IsValid(a.x, a.y));

        return neighbours;

        bool IsValid(int x, int y)
        {
            try
            {
                if (previousPath.Contains((x, y))) return false;

                var a = input[i][j] == 'S' ? 0 : input[i][j] - 'a';
                var b = input[x][y] == 'E' ? 'z' - 'a' : input[x][y] - 'a';

                if (a + 1 >= b)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
