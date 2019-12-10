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

            //Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(List<string> input)
        {
            return input
                .ConvertMap()
                .CheckMaxAsteroids()
                .ToString();
        }
        public static string Task2(List<string> input)
        {
            var asteroidData = input
                .ConvertMap()
                .CountAsteroids((19, 14));
                // .Select(z =>
                // {
                //     if(z.x)
                // });
            return "";
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
        public static int OrderValue()
    }
}
