using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Rectangle> input = File.ReadLines("input.txt").Select(x => new Rectangle(x)).ToList();
            HashSet<string> area = new HashSet<string>();
            HashSet<string> matchedArea = new HashSet<string>();
            int areaCounter = 0;
            foreach (var value in input)
            {
                for (int i = 0; i < value.w; i++)
                {
                    for (int j = 0; j < value.h; j++)
                    {
                        var t = $"{ value.x + i} { value.y + j}";
                        if (area.Contains(t))
                        {
                            if (!matchedArea.Contains(t))
                            {
                                matchedArea.Add(t);
                                areaCounter++;
                            }
                        }
                        else area.Add(t);
                    }
                }
            }
            Console.WriteLine(areaCounter);

            foreach (var value in input)
            {
                var flag = false;
                for (int i = 0; i < value.w; i++)
                {
                    for (int j = 0; j < value.h; j++)
                    {
                        var t = $"{ value.x + i} { value.y + j}";
                        if (matchedArea.Contains(t))
                        {
                            flag = true;
                        }
                    }
                }
                if (!flag) Console.WriteLine(value.id);
            }
        }

        public class Rectangle
        {
            public int id;
            public int x;
            public int y;
            public int w;
            public int h;
            public Rectangle(string s)
            {
                var a = s.Split(' ', '#', '@', ',', ':', 'x').Where(x => !String.IsNullOrWhiteSpace(x)).ToList();
                id = Int32.Parse(a[0]);
                x = Int32.Parse(a[1]);
                y = Int32.Parse(a[2]);
                w = Int32.Parse(a[3]);
                h = Int32.Parse(a[4]);
            }
        }
    }
}
