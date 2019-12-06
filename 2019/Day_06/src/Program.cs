using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_06
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();

            Console.WriteLine(Tasks(input));
        }

        public static (int, int) Tasks(List<string> input)
        {
            var map = new Dictionary<string, (int depth, List<string> parents)> { { "COM", (0, new List<string>()) } };

            var planets = input.Select(y =>
            {
                return (parent: y.Substring(0, 3), child: y.Substring(4, 3));
            }).ToList();

            BuildMap("COM");

            var task1 = map.Sum(x => x.Value.depth);
            var san = map["SAN"];
            var you = map["YOU"];

            int commonParentDepth = 0;

            for (int i = 0; ; i++)
            {
                if (san.parents[i] != you.parents[i])
                {
                    commonParentDepth = i;
                    break;
                }
            }

            var task2 = san.depth + you.depth - 2 * commonParentDepth;

            return (task1, task2);

            void BuildMap(string parentKey)
            {
                var parent = map[parentKey];

                var children = planets.Where(t => t.parent == parentKey).Select(t => t.child);

                if (children.Count() > 0)
                {
                    var parentList = new List<string>(parent.parents);
                    parentList.Add(parentKey);
                    var depth = parent.depth + 1;
                    foreach (var child in children)
                    {
                        map.Add(child, (depth, parentList));
                        BuildMap(child);
                    }
                }
            }
        }
    }
}
