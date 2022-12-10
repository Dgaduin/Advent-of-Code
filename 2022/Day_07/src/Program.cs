using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Day_07;
public static class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadLines("input.txt").ToList();
        var root = input.ParseInput();

        Console.WriteLine(Task1(root));
        Console.WriteLine(Task2(root));
    }

    public static int Task1(Directory root)
        => root.GetChildDirectoriesFlat().Where(x => x.Size <= 100000).Sum(x => x.Size);

    public static int Task2(Directory root)
    {
        var remainingSize = 30000000 - (70000000 - root.Size);
        return root.GetChildDirectoriesFlat()
                   .Where(x => x.Size >= remainingSize)
                   .OrderBy(x => x.Size)
                   .First()
                   .Size;
    }

    public static Directory ParseInput(this List<string> input)
    {
        var root = new Directory() { Name = "/" };
        Directory current = root;
        foreach (var line in input)
        {
            if (line[0] == '$')
            {
                if (line == "$ cd /" || line[2] == 'l') continue;
                var dirName = line.Split(' ')[2];
                if (dirName == "..") current = current.Parent;
                else
                {
                    var newDir = current.GetByName(dirName);
                    current = newDir;
                }
            }
            else
            {
                var splitLine = line.Split(' ');
                if (splitLine[0] == "dir")
                {
                    var dir = new Directory() { Name = splitLine[1], Parent = current };
                    current.Nodes.Add(dir);
                }
                else
                {
                    var file = new NFile() { Name = splitLine[1], Size = Int32.Parse(splitLine[0]) };
                    current.Nodes.Add(file);
                }
            }
        }
        return root;
    }
}

public interface Node
{
    string Name { get; set; }
    int Size { get; }
}

public class Directory : Node
{
    public Directory Parent { get; set; }
    public string Name { get; set; }
    public List<Node> Nodes { get; set; } = new List<Node>();
    public int Size => Nodes.Sum(x => x.Size);
    public Directory GetByName(string name) => (Directory)Nodes.First(x => x is Directory && x.Name == name);

    public List<Directory> GetChildDirectoriesFlat()
    {
        var list = new List<Directory>();
        list.Add(this);

        foreach (var node in Nodes)
            if (node is Directory)
                list.AddRange(((Directory)node).GetChildDirectoriesFlat());

        return list;
    }
}

public class NFile : Node
{
    public string Name { get; set; }

    public int Size { get; set; }
}