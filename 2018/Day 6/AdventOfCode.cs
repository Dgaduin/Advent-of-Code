using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    // https://adventofcode.com/2018/day/6
    public class AdventOfCode
    {

        public static void Main()
        {
            IEnumerable<string> input = File.ReadAllLines("input.txt");
            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(IEnumerable<string> input)
        {
            List<Point> points = input.Select((x, i) =>
              {
                  var temp = x.Split(',', StringSplitOptions.RemoveEmptyEntries);
                  return new Point { X = Int32.Parse(temp[0]), Y = Int32.Parse(temp[1]), Id = i };
              }).ToList();

            IEnumerable<int> listX = points.Select(x => x.X);
            IEnumerable<int> listY = points.Select(x => x.Y);

            int maxX = listX.Max();
            int minX = listX.Min();
            int maxY = listY.Max();
            int minY = listY.Min();

            Dictionary<MiniPoint, Point> map = new Dictionary<MiniPoint, Point>();

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    int min = Int32.MaxValue;
                    int count = 0;
                    Point minPoint = null;
                    foreach (var point in points)
                    {
                        int distance = Math.Abs(point.X - i) + Math.Abs(point.Y - j);
                        if (distance < min)
                        {
                            min = distance;
                            count = 1;
                            minPoint = point;
                        }
                        else if (distance == min)
                            count++;
                    }
                    if (count > 1)
                        map.Add(new MiniPoint { X = i, Y = j }, new Point { Id = -1 });
                    else
                        map.Add(new MiniPoint { X = i, Y = j }, minPoint);
                }
            }

            var infiList = map.Where(x => x.Key.X == minX || x.Key.X == maxX || x.Key.Y == minY || x.Key.Y == maxY).Select(x => x.Value.Id).Distinct();
            return points.Where(x => !infiList.Contains(x.Id)).Select(x => map.Count(y => y.Value.Id == x.Id)).Max().ToString();
        }

        public static string Task2(IEnumerable<string> input)
        {
            List<Point> points = input.Select((x, i) =>
             {
                 var temp = x.Split(',', StringSplitOptions.RemoveEmptyEntries);
                 return new Point { X = Int32.Parse(temp[0]), Y = Int32.Parse(temp[1]), Id = i };
             }).ToList();

            IEnumerable<int> listX = points.Select(x => x.X);
            IEnumerable<int> listY = points.Select(x => x.Y);

            int maxX = listX.Max();
            int minX = listX.Min();
            int maxY = listY.Max();
            int minY = listY.Min();

            int regionSize = 0;

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    int sum = 0;
                    foreach (var point in points)
                    {
                        sum += Math.Abs(point.X - i) + Math.Abs(point.Y - j);
                    }
                    if (sum < 10000) regionSize++;
                }
            }

            return regionSize.ToString();
        }
    }

    public struct MiniPoint
    {
        public int X;
        public int Y;
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }
    }
}
