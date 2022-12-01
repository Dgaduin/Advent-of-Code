using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();
            // string input = File.ReadAllText("input.txt");
            var elfs = ChunkIntoElfs(input).ToList();

            Console.WriteLine(Task1(elfs));
            Console.WriteLine(Task2(elfs));
            var temp1 = elfs.Select(x => x.Sum());
            var temp2 = temp1.OrderByDescending(x => x);
            var temp3 = temp2.Take(3);
            var temp4 = temp3.Sum();
        }

        public static int Task1(List<List<int>> input) => input.Select(x => x.Sum()).Max();
        public static int Task2(List<List<int>> input) => input.Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum();

        public static IEnumerable<List<int>> ChunkIntoElfs(List<string> input)
        {
            var temp = new List<int>();
            foreach (var row in input)
            {
                if (row != "")
                    temp.Add(Int32.Parse(row));
                else
                {
                    yield return temp;
                    temp = new List<int>();
                }
            }
            yield return temp;
            yield break;
        }
    }
}
