using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_11;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();

        var chunks = input.Chunk(7);
        var monkeys = chunks.Select(ParseInput).ToDictionary(x => x.Id, x => x);

        //Console.WriteLine(Task1(monkeys));
        Console.WriteLine(Task2(monkeys));
    }

    public static long Task1(Dictionary<int, Monkey> monkeys)
    {
        for (int i = 1; i <= 20; i++)
        {
            foreach (var monkey in monkeys.Values)
            {
                foreach (var item in monkey.Items)
                {
                    monkey.Inspections++;
                    var mappedItem = monkey.ExecuteOperation(item) / 3;
                    var newMonkey = monkey.TestItem(mappedItem);
                    monkeys[newMonkey].Items.Add(mappedItem);
                }
                monkey.Items.Clear();
            }
        }
        var temp = monkeys.Values.OrderByDescending(x => x.Inspections).ToList();

        return temp[0].Inspections * temp[1].Inspections;
    }

    public static long Task2(Dictionary<int, Monkey> monkeys)
    {
        var div = monkeys.Values.Aggregate(1, (x, y) => x *= y.Divisor);
        for (int i = 1; i <= 10000; i++)
        {
            foreach (var monkey in monkeys.Values)
            {
                foreach (var item in monkey.Items)
                {
                    monkey.Inspections++;
                    var mappedItem = monkey.ExecuteOperation(item % div);
                    var newMonkey = monkey.TestItem(mappedItem);
                    monkeys[newMonkey].Items.Add(mappedItem);
                }
                monkey.Items.Clear();
            }
            if (i == 1 || i == 20 || i == 1000 || i == 2000 || i == 3000 || i == 4000 || i == 5000 || i == 6000 || i == 7000 || i == 8000 || i == 9000 || i == 10000)
            {
                Console.WriteLine($"== After round {i} ==");
                var t = monkeys.Values.Select(x => $"Monkey {x.Id} inspected items {x.Inspections} times.");
                foreach (var m in t) Console.WriteLine(m);
            }
        }
        var temp = monkeys.Values.OrderByDescending(x => x.Inspections).ToList();

        return temp[0].Inspections * temp[1].Inspections;
    }

    public static Monkey ParseInput(string[] input)
    {
        var monkey = new Monkey();

        monkey.Id = Int32.Parse(input[0].Split(' ', ':')[1]);

        var itemSplit = input[1].Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
        monkey.Items.AddRange(itemSplit[2..].Select(long.Parse));

        var opSplit = input[2].Split(' ');
        var lastParam = opSplit[^1];
        var opParam = opSplit[^2];
        Func<long, long> op = (opParam, lastParam) switch
        {
            ("*", "old") => (long x) => x * x,
            ("+", "old") => (long x) => x + x,
            ("*", _) => (long x) => x * long.Parse(lastParam),
            ("+", _) => (long x) => x + long.Parse(lastParam)
        };
        monkey.Operation = op;

        var divs = Int32.Parse(input[3].Split(' ')[^1]);
        monkey.Divisor = divs;

        var trueState = Int32.Parse(input[4].Split(' ')[^1]);
        monkey.TrueMonkey = trueState;

        var falseState = Int32.Parse(input[5].Split(' ')[^1]);
        monkey.FalseMonkey = falseState;

        return monkey;
    }
}

public class Monkey
{
    public int Id { get; set; }
    public List<long> Items { get; set; } = new List<long>();
    public Func<long, long> Operation { get; set; }
    public int Divisor { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
    public long Inspections { get; set; }

    public int TestItem(long x) => x % Divisor == 0 ? TrueMonkey : FalseMonkey;
    public long ExecuteOperation(long x) => Operation(x);
}
