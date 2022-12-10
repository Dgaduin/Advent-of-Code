using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Day_10;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        // string input = File.ReadAllText("input.txt");
        var stack = RunProgram(input);
        Console.WriteLine(Task1(stack));
        Task2(stack);
    }

    public static int Task1(List<int> input)
    {
        int sum = 0;
        for (int i = 19; i < input.Count; i += 40)
            sum += input[i] * (i + 1);
        return sum;
    }

    public static void Task2(List<int> input)
    {
        var chunks = input.Chunk(40);
        foreach (var chunk in chunks)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < chunk.Length; i++)
            {
                if (i <= chunk[i] + 1 && i >= chunk[i] - 1)
                    sb.Append('#');
                else sb.Append('.');
            }
            Console.WriteLine(sb);
        }
    }

    public static List<int> RunProgram(List<string> input)
    {
        var stack = new List<int> { 1 };
        var commands = input.Select(x => x.Split(' '));
        foreach (var row in commands)
        {
            var last = stack[^1];
            if (row[0] == "noop")
                stack.Add(last);
            if (row[0] == "addx")
            {
                stack.Add(last);
                stack.Add(last + Int32.Parse(row[1]));
            }
        }
        return stack;
    }
}
