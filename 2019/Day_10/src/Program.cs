using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_10
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines("input.txt").ToList();

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input, (19, 14)));
        }

        public static string Task1(List<string> input)
        {
            return input
                .ConvertMap()
                .CheckMaxAsteroids()
                .ToString();
        }
        public static string Task2(List<string> input, (int, int) origin)
        {
            var t = input
                .ConvertMap()
                .CountAsteroids(origin)
                .OrderBy(z =>
                    z.Key switch
                    {
                        var (x, y) when x >= 0 && y < 0 => Math.Atan2(Math.Abs(x), Math.Abs(y)),
                        var (x, y) when x > 0 && y >= 0 => Math.Atan2(Math.Abs(y), Math.Abs(x)) + 0.5 * Math.PI,
                        var (x, y) when x <= 0 && y > 0 => Math.Atan2(Math.Abs(x), Math.Abs(y)) + Math.PI,
                        var (x, y) when x < 0 && y <= 0 => Math.Atan2(Math.Abs(y), Math.Abs(x)) + 1.5 * Math.PI,
                    }
                )
                .ToList();

            var asteroidData = t[199];

            return (asteroidData.Value.x * 100 + asteroidData.Value.y).ToString();
        }


    }
    public static class Methods
    {
        public static List<(int x, int y)> ConvertMap(this List<string> input)
        {
            var map = new List<(int x, int y)>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == '#')
                        map.Add((x: j, y: i));
                }
            }
            return map;
        }

        public static int CheckMaxAsteroids(this List<(int x, int y)> map)
        {
            int max = 0;
            (int, int) maxCords = (0, 0);
            foreach (var asteroid in map)
            {
                var copy = new List<(int x, int y)>(map);
                var count = copy.CountAsteroids(asteroid).Keys.Count;
                if (count > max)
                {
                    max = count;
                    maxCords = asteroid;
                }
            }
            Console.WriteLine(maxCords);
            return max;
        }

        public static Dictionary<(int, int), (int x, int y)> CountAsteroids(this List<(int x, int y)> map, (int x, int y) origin)
        {
            map.Remove(origin);

            var a = map.Select(asteroid =>
            {
                var direction = (
                    x: asteroid.x - origin.x,
                    y: asteroid.y - origin.y);

                var gcd = GCD(direction.x, direction.y);
                return gcd == 0
                    ? new KeyValuePair<(int, int), (int x, int y)>((direction.x, direction.y), asteroid)
                    : new KeyValuePair<(int, int), (int x, int y)>((direction.x / gcd, direction.y / gcd), asteroid);
            })
            .GroupBy(z => z.Key)
            .ToDictionary(
                z => z.Key,
                z => z.First().Value
            );

            return a;
        }

        public static int GCD(int n, int m)
        {
            n = Math.Abs(n);
            m = Math.Abs(m);


            if (n < m)
            {
                int tmp = n;
                n = m;
                m = tmp;
            }

            while (m > 0)
            {
                int tmp = n % m;
                n = m;
                m = tmp;
            }

            return n;
        }

        public static int Quadrant(this (int, int) point)
        {
            return point switch
            {
                var (x, y) when x > 0 && y > 0 => 2,
                var (x, y) when x < 0 && y > 0 => 3,
                var (x, y) when x < 0 && y < 0 => 4,
                var (x, y) when x > 0 && y < 0 => 1,
                _ => 1
            };
        }
    }
}
