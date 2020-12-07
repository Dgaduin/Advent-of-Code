using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_07
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(List<string> input)
        {
            var dict = new Dictionary<string, Dictionary<string, int>>();
            foreach (var x in input)
            {
                Dictionary<string, int> set;

                var splitInput = x.Split(' ').ToList();
                var parent = $"{splitInput[0]} {splitInput[1]}";

                if (dict.ContainsKey(parent))
                {
                    set = dict[parent];
                }
                else
                {
                    set = new Dictionary<string, int>();
                    dict[parent] = set;
                }

                if (splitInput.Count % 2 != 1)
                    for (int i = 1; i < splitInput.Count / 4; i++)
                    {
                        var child = $"{splitInput[i * 4 + 1]} {splitInput[i * 4 + 2]}";
                        var childCount = Int32.Parse(splitInput[i * 4]);
                        set.Add(child, childCount);
                    }
            }

            return GetCount(dict, "shiny gold").Count;
        }

        public static int Task2(List<string> input)
        {
            var dict = new Dictionary<string, Dictionary<string, int>>();
            foreach (var x in input)
            {
                Dictionary<string, int> set;

                var splitInput = x.Split(' ').ToList();
                var parent = $"{splitInput[0]} {splitInput[1]}";

                if (dict.ContainsKey(parent))
                {
                    set = dict[parent];
                }
                else
                {
                    set = new Dictionary<string, int>();
                    dict[parent] = set;
                }

                if (splitInput.Count % 2 != 1)
                    for (int i = 1; i < splitInput.Count / 4; i++)
                    {
                        var child = $"{splitInput[i * 4 + 1]} {splitInput[i * 4 + 2]}";
                        var childCount = Int32.Parse(splitInput[i * 4]);
                        set.Add(child, childCount);
                    }
            }

            return GetNestedCount(dict, "shiny gold");
        }

        private static int GetNestedCount(Dictionary<string, Dictionary<string, int>> dict, string key)
        {
            var list = dict[key];
            int count = 0;
            foreach (var rule in list)
            {
                count += GetNestedCount(dict, rule.Key) * rule.Value + rule.Value;
            }
            return count;
        }

        private static HashSet<string> GetCount(Dictionary<string, Dictionary<string, int>> dict, string key)
        {
            var list = new HashSet<string>();
            foreach (var bag in dict)
            {
                if (bag.Value.Keys.Contains(key))
                    list.Add(bag.Key);
            }
            var tempList = new HashSet<string>();
            foreach (var newKey in list)
            {
                tempList.UnionWith(GetCount(dict, newKey));
            }
            list.UnionWith(tempList);
            return list;
        }
    }
}
