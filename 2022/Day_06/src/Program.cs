using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_06;
public static class Program
{
    static void Main(string[] args)
    {
        string input = File.ReadAllText("input.txt");

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2(input));
    }

    public static int Task1(string input) => ExtractIndexOfUniqueSize(input, 4);

    public static int Task2(string input) => ExtractIndexOfUniqueSize(input, 14);

    private static int ExtractIndexOfUniqueSize(string input, int size)
    {
        for (int i = 0; i < input.Length - size; i++)
            if (input[i..(i + size)].ToHashSet().Count == size) return i + size;
        return 0;
    }
}
