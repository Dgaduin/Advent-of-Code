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
            Console.WriteLine(Task2(input));
        }
        public static string Task1(List<string> input)
        {
            var wire1 = GenerateWirePath(input[0]);
            var wire2 = GenerateWirePath(input[1]);

            return wire1.Keys
                .Intersect(wire2.Keys)
                .Min(x => Math.Abs(x.x) + Math.Abs(x.y))
                .ToString();
        }
        public static string Task2(List<string> input)
        {
            var wire1 = GenerateWirePath(input[0]);
            var wire2 = GenerateWirePath(input[1]);

            return wire1.Keys
                .Intersect(wire2.Keys)
                .Min(x => (wire1[x] + wire2[x] + 2))
                .ToString();
        }

        static Dictionary<(int x, int y), int> GenerateWirePath(string value)
        {
            var values = value.Split(',');
            int x = 0, y = 0, length = 0;
            var map = new Dictionary<(int x, int y), int>();

            foreach (var wire in values)
            {
                char direction = wire[0];
                var distance = Int32.Parse(wire.Substring(1));

                if (direction == 'R')
                {
                    for (int i = 0; i < distance; i++)
                    {
                        map.TryAdd((x: ++x, y: y), length++);
                    }
                }
                if (direction == 'L')
                {
                    for (int i = 0; i < distance; i++)
                    {
                        map.TryAdd((x: --x, y: y), length++);
                    }
                }
                if (direction == 'U')
                {
                    for (int i = 0; i < distance; i++)
                    {
                        map.TryAdd((x: x, y: ++y), length++);
                    }

                }
                if (direction == 'D')
                {
                    for (int i = 0; i < distance; i++)
                    {
                        map.TryAdd((x: x, y: --y), length++);
                    }
                }
            }
            return map;
        }
    }
}
