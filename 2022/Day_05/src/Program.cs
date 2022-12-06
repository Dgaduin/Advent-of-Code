using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_05;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");

        var parsedInput = input.ParseInput();
        var parsedInput2 = input.ParseInput();

        Console.WriteLine(Task1(parsedInput));
        Console.WriteLine(Task2(parsedInput2));
    }

    public static (Stack<char>[] stacks, IEnumerable<(int, int, int)> commands) ParseInput(this List<string> input)
    {
        var index = input.FindIndex(x => x == "");

        var stackCount = input[index - 1].Last() - '0';

        var map = input.Take(index - 1).ToList();
        var stacks = new Stack<char>[stackCount];

        for (int i = 0; i < stackCount; i++)
        {
            var stack = new Stack<char>();
            for (int j = map.Count - 1; j >= 0; j--)
            {
                var k = i * 4 + 1;
                if (k > map[j].Length) break;
                var c = map[j][k];
                if (c != ' ')
                    stack.Push(c);
                else break;
            }
            stacks[i] = stack;
        }


        var regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
        var commands = input
                        .Skip(index + 1)
                        .Select(x =>
                        {
                            var match = regex.Match(x);
                            return (Int32.Parse(match.Groups[1].Value),
                                    Int32.Parse(match.Groups[2].Value) - 1,
                                    Int32.Parse(match.Groups[3].Value) - 1);
                        });
        return (stacks, commands);
    }

    public static string Task1((Stack<char>[] stacks, IEnumerable<(int count, int from, int to)> commands) input)
    {
        foreach (var command in input.commands)
        {
            for (int i = 0; i < command.count; i++)
            {
                var t = input.stacks[command.from].Pop();
                input.stacks[command.to].Push(t);
            }
        }

        return new String(input.stacks.Select(x => x.Pop()).ToArray());
    }

    public static string Task2((Stack<char>[] stacks, IEnumerable<(int count, int from, int to)> commands) input)
    {
        foreach (var command in input.commands)
        {
            var temp = new Stack<char>();

            for (int i = 0; i < command.count; i++)
            {
                var t = input.stacks[command.from].Pop();
                temp.Push(t);
            }

            for (int i = 0; i < command.count; i++)
            {
                var t = temp.Pop();
                input.stacks[command.to].Push(t);
            }
        }

        return new String(input.stacks.Select(x => x.Pop()).ToArray());
    }
}
