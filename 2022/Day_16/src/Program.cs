﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Day_16;
public static class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").ParseInput();

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2());
    }

    public static int Task1(Dictionary<string, Node> input)
    {
        var non0Nodes = input.Values.Where(x => x.Valve > 0).ToList();

        foreach (var node in input.Values)
        {
            node.SetBest(node.Id, 0);
            foreach (var child in node.Nodes) node.SetBest(child.Id, 1);
        }

        foreach (var node in input.Values) BestPath(node, node.BestPaths);

        return FindBestSmart(input["AA"], 0, 1, new HashSet<string>());

        // int FindBestSmart(Node node, int pressure, int round, HashSet<string> visited)
        // {
        //     node.IsOn = true;
        //     visited.Add(node.Id);
        //     var newPressure = pressure + ((31 - round) * node.Valve);

        //     Console.WriteLine($"{round}:{visited.PrintList()}:{newPressure}");

        //     if (AreAllOn() || round > 30)
        //     {
        //         visited.Remove(node.Id);
        //         node.IsOn = false;
        //         return newPressure;
        //     }

        //     var bestOption1 = non0Nodes
        //         .Where(x => !visited.Contains(x.Id))
        //         .OrderByDescending(x =>
        //             {
        //                 var t = 31 - (round + node.BestPaths[x.Id] + 1);
        //                 t = t * x.Valve;
        //                 Console.Write($"{x.Id}:{t}:");
        //                 t = t * 100 + node.BestPaths[x.Id];
        //                 Console.WriteLine(t);
        //                 return t;
        //             }).ToList();

        //     var bestOption = bestOption1.First();

        //     var r = FindBest(bestOption, newPressure, round + node.BestPaths[bestOption.Id] + 1, visited);

        //     visited.Remove(node.Id);
        //     node.IsOn = false;
        //     return r;
        // }

        int FindBest(Node node, int pressure, int round, HashSet<string> visited)
        {
            node.IsOn = true;
            visited.Add(node.Id);
            var newPressure = pressure + ((31 - round) * node.Valve);

            Console.WriteLine($"{round}:{visited.PrintList()}:{newPressure}");

            if (AreAllOn() || round > 30)
            {
                visited.Remove(node.Id);
                node.IsOn = false;
                return newPressure;
            }

            var remaining = non0Nodes.Where(x => !visited.Contains(x.Id)).ToList();

            var returnCollection = new List<int>();
            foreach (var row in remaining)
            {
                var r = FindBest(row, newPressure, round + node.BestPaths[row.Id] + 1, visited);
                returnCollection.Add(r);
            }

            visited.Remove(node.Id);
            node.IsOn = false;
            return returnCollection.Any() ? returnCollection.Max() : 0;
        }

        bool AreAllOn() => non0Nodes.All(x => x.IsOn);

        void BestPath(Node node, Dictionary<string, int> best, int i = 0)
        {
            if (i > 6) return;
            foreach (var row in best) node.SetBest(row.Key, row.Value + 1);
            foreach (var child in node.Nodes) BestPath(child, node.BestPaths, i + 1);
        }
    }

    public static string PrintList<T>(this IEnumerable<T> list)
    {
        var sb = new StringBuilder();
        foreach (var t in list) sb.Append(t);
        return sb.ToString();
    }

    public static Dictionary<string, Node> ParseInput(this IEnumerable<string> input)
    {
        var nodes = new Dictionary<string, Node>();
        var regex = new Regex("Valve (\\w{2}) has flow rate=(\\d+); tunnel[ s]*lead[ s]*to valve[ s]*([A-Z ,]+)");
        foreach (var row in input)
        {
            var match = regex.Match(row);
            var node = new Node()
            {
                Id = match.Groups[1].Value,
                Valve = Int32.Parse(match.Groups[2].Value),
                StringNodes = match.Groups[3].Value,
            };
            nodes.Add(node.Id, node);
        }
        foreach (var node in nodes.Values)
        {
            var split = node.StringNodes.Split(", ");
            foreach (var child in split)
                node.Nodes.Add(nodes[child]);
        }
        return nodes;
    }
}

public class Node
{
    public string Id { get; set; }
    public int Valve { get; set; }
    public bool IsOn { get; set; } = false;
    public List<Node> Nodes { get; set; } = new List<Node>();
    public string StringNodes { get; set; }
    public Dictionary<string, int> BestPaths { get; set; } = new Dictionary<string, int>();
    public void SetBest(string key, int value)
    {
        if (BestPaths.TryGetValue(key, out int oldValue))
            BestPaths[key] = Math.Min(value, oldValue);
        else
            BestPaths[key] = value;
    }
}