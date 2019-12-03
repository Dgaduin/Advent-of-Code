using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2());
        }

        static HashSet<(int x, int y)> GenerateWirePath(string value)
        {
            var values = value.Split(',');
            (int x, int y) position = (0, 0);
            var paths = new HashSet<(int x, int y)>();
            foreach (var wire in values)
            {
                var direction = wire.Substring(0, 1);
                var distance = Int32.Parse(wire.Substring(1));
                IEnumerable<(int x, int y)> newPositions = null;
                switch (direction)
                {
                    case "R":
                        {
                            newPositions = Enumerable.Range(position.x, distance).Select(w => (x: w, y: position.y));
                            position.x += distance;
                            break;
                        }
                    case "L":
                        {
                            newPositions = Enumerable.Range(position.x - distance, distance).Select(w => (x: w, y: position.y));
                            position.x -= distance;
                            break;
                        }
                    case "U":
                        {
                            newPositions = Enumerable.Range(position.y, distance).Select(w => (x: position.x, y: w));
                            position.y += distance;
                            break;
                        }
                    case "D":
                        {
                            newPositions = Enumerable.Range(position.y - distance, distance).Select(w => (x: position.x, y: w));
                            position.y -= distance;
                            break;
                        }
                };
                paths.UnionWith(newPositions);
            }
            paths.Remove((0, 0));
            return paths;
        }

        public static string Task1(List<string> input)
        {
            var wire1 = GenerateWirePath(input[0]);
            var wire2 = GenerateWirePath(input[1]);

            return wire1
                .Intersect(wire2)
                .Select(x => Math.Abs(x.x) + Math.Abs(x.y))
                .Min()
                .ToString();
        }
        public static string Task2() { return ""; }
    }
}
