using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.Json.Nodes;
//6772
//6566
//6656
namespace Day_13;
public static class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").Where(x => x != "").Chunk(2);

        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2(input));
    }

    public static int Task1(IEnumerable<string[]> input)
    {
        var pairs = input.Select(ParseInput).ToList();
        var sum = 0;
        for (int i = 0; i < pairs.Count; i++)
        {
            var t = CompareData(pairs[i].Item1, pairs[i].Item2);
            if (t.HasValue && t.Value == true)
                sum += (i + 1);
        }
        return sum;
    }
    public static int Task2(IEnumerable<string[]> input)
    {
        var pairs = input.SelectMany(x => { var t = ParseInput(x); return new[] { t.Item1, t.Item2 }; }).ToList();
        var divider1 = ParseLine("[[2]]");
        var divider2 = ParseLine("[[6]]");
        pairs.Add(divider1);
        pairs.Add(divider2);

        var x = pairs.OrderByDescending(x => x, new DataComparer()).ToList();
        var a = x.IndexOf(divider1) + 1;
        var b = x.IndexOf(divider2) + 1;
        return a * b;
    }

    public static bool? CompareData(Data a, Data b)
    {
        if (a.IntValue != -1 && b.IntValue != -1)
        {
            if (a.IntValue < b.IntValue) return true;
            else if (a.IntValue > b.IntValue) return false;
            else return null;
        }

        if (a.ListValue == null) a.AddListValue(a.IntValue);
        if (b.ListValue == null) b.AddListValue(b.IntValue);

        for (int i = 0; i <= b.ListValue.Count; i++)
        {
            if (i >= a.ListValue.Count && i < b.ListValue.Count) return true;
            if (i >= b.ListValue.Count && i < a.ListValue.Count) return false;
            if (i == b.ListValue.Count) return null;
            var result = CompareData(a.ListValue[i], b.ListValue[i]);
            if (result.HasValue) return result.Value;
        }

        return null;
    }

    public static (Data, Data) ParseInput(string[] input) => (list1: ParseLine(input[0]), list2: ParseLine(input[1]));

    public static Data ParseLine(string line)
    {
        var numberBuffer = new StringBuilder();
        var returnBuffer = new Data() { IntValue = -1, ListValue = new List<Data>() };
        var currentList = returnBuffer;

        for (int i = 1; i < line.Length; i++)
        {
            var c = line[i];
            if (c == '[')
                currentList = currentList.AddListValue(null, new List<Data>());
            else if (c == ']')
            {
                if (numberBuffer.Length > 0)
                {
                    currentList.AddListValue(Int32.Parse(numberBuffer.ToString()));
                    numberBuffer.Clear();
                }
                currentList = currentList.Parent;
            }
            else if ((c - '0') <= 9 && (c - '0') >= 0)
                numberBuffer.Append(c);
            else if (c == ',')
            {
                if (numberBuffer.Length > 0)
                {
                    currentList.AddListValue(Int32.Parse(numberBuffer.ToString()));
                    numberBuffer.Clear();
                }
            }
        }
        return returnBuffer;
    }
}

public class DataComparer : IComparer<Data>
{
    public int Compare(Data a, Data b) =>
        Program.CompareData(a, b) switch
        {
            true => 1,
            false => -1,
            null => 0
        };
}

public class Data
{
    public int IntValue { get; set; } = -1;
    public List<Data> ListValue { get; set; }
    public Data Parent { get; set; }

    public Data AddListValue(int? intValue = null, List<Data> listValue = null)
    {
        var value = GenerateChild(intValue, listValue);
        if (ListValue == null) ListValue = new List<Data>();
        ListValue.Add(value);
        return value;
    }

    public Data GenerateChild(int? intValue = null, List<Data> listValue = null) => intValue switch
    {
        not null => new Data() { IntValue = intValue.Value, Parent = this },
        _ => new Data() { ListValue = listValue, Parent = this }
    };
}
