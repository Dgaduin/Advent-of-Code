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
        var games = GetGames(input);

        Console.WriteLine(Task1(games));
        Console.WriteLine(Task2(games));
    }

    /*
    Rock     = 0
    Paper    = 1
    Scissors = 2
    Loss     = 0
    Draw     = 1
    Win      = 2
    */
    public static int Task1(List<(int, int)> games)
    {
        int score = 0;
        foreach (var row in games)
        {
            score += row switch
            {
                (0, 0) or (1, 1) or (2, 2) => 3,
                (0, 2) or (1, 0) or (2, 1) => 0,
                (0, 1) or (1, 2) or (2, 0) => 6,
            };
            score += (row.Item2 + 1);
        }
        return score;
    }
    public static int Task2(List<(int, int)> games)
    {
        int score = 0;
        foreach (var row in games)
        {
            score += row switch
            {
                (1, 0) or (0, 1) or (2, 2) => 1,
                (2, 0) or (1, 1) or (0, 2) => 2,
                (0, 0) or (2, 1) or (1, 2) => 3
            };
            score += (row.Item2 * 3);
        }
        return score;
    }

    public static List<(int, int)> GetGames(List<string> input)
        => input
            .Select(x => (x[0] - 'A', x[2] - 'X'))
            .ToList();
}
