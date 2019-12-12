using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_12
{
    public class Program
    {
        static void Main(string[] args)
        {
            var r = new Regex("-?\\d+");
            var input = File
                .ReadLines("input.txt")
                .Select(z => r.Matches(z))
                .Select(z => new Moon
                {
                    x = Int32.Parse(z[0].Value),
                    y = Int32.Parse(z[1].Value),
                    z = Int32.Parse(z[2].Value),
                })
                .ToList();

            input = new List<Moon>{
                new Moon{x = -8,y =-10,z=  0},
                new Moon{x=  5, y=  5, z= 10},
                new Moon{x=  2, y= -7, z=  3},
                new Moon{x=  9, y= -8, z= -3}
            };

            //Console.WriteLine(Task1(input, 1000));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(List<Moon> moons, int steps = 1000)
        {
            CalculatePosition(moons, steps);
            return moons.Sum(z => z.energy).ToString();
        }

        public static string Task2(List<Moon> moons)
        {
            var pairs = new (int, int)[]{
                (0,1),
                (0,2),
                (0,3),
                (1,2),
                (1,3),
                (2,3)
            };

            bool xComplete = false;
            var xPrev = $"{moons[0].x} {moons[1].x} {moons[2].x} {moons[3].x}";
            int xCount = 0;
            var xSet = new HashSet<string>();

            bool yComplete = false;
            var yPrev = $"{moons[0].y} {moons[1].y} {moons[2].y} {moons[3].y}";
            int yCount = 0;
            var ySet = new HashSet<string>();

            bool zComplete = false;
            var zPrev = $"{moons[0].z} {moons[1].z} {moons[2].z} {moons[3].z}";
            int zCount = 0;
            var zSet = new HashSet<string>();
            bool zFlag = false;

            int count = 0;
            while (!xComplete || !yComplete || !zComplete)
            {
                foreach (var pair in pairs)
                {
                    var a = moons[pair.Item1];
                    var b = moons[pair.Item2];

                    UpdateVelocity(a, b);

                }
                moons.ForEach(z => z.ApplyVelocity());

                if (!xComplete)
                {
                    xSet.Add(xPrev);
                    xCount++;
                    xPrev = $"{moons[0].x} {moons[1].x} {moons[2].x} {moons[3].x}";
                    if (xSet.Contains(xPrev))
                    {
                        xComplete = true;
                    }
                }
                if (!yComplete)
                {
                    ySet.Add(yPrev);
                    yCount++;
                    yPrev = $"{moons[0].y} {moons[1].y} {moons[2].y} {moons[3].y}";
                    if (ySet.Contains(yPrev))
                    {
                        yComplete = true;
                    }
                }
                if (!zComplete)
                {
                    zSet.Add(zPrev);
                    zPrev = $"{moons[0].z} {moons[1].z} {moons[2].z} {moons[3].z}";
                    if (zSet.Contains(zPrev) && zFlag)
                    {
                        zComplete = true;
                        zCount = count - zCount;
                    }
                    if (zSet.Contains(zPrev) && !zFlag)
                    {
                        zFlag = true;
                        zCount = count;
                    }
                }
                count++;
            }

            return $"{xCount} {yCount} {zCount}";
        }

        public static void CalculatePosition(List<Moon> moons, int steps)
        {
            var pairs = new (int, int)[]{
                (0,1),
                (0,2),
                (0,3),
                (1,2),
                (1,3),
                (2,3)
            };

            for (int i = 0; i < steps; i++)
            {
                foreach (var pair in pairs)
                {
                    var a = moons[pair.Item1];
                    var b = moons[pair.Item2];

                    UpdateVelocity(a, b);

                }
                moons.ForEach(z => z.ApplyVelocity());
            }
        }

        public static void UpdateVelocity(Moon a, Moon b)
        {
            if (a.x > b.x)
            {
                a.vx--;
                b.vx++;
            }
            else if (a.x < b.x)
            {
                a.vx++;
                b.vx--;
            }

            if (a.y > b.y)
            {
                a.vy--;
                b.vy++;
            }
            else if (a.y < b.y)
            {
                a.vy++;
                b.vy--;
            }

            if (a.z > b.z)
            {
                a.vz--;
                b.vz++;
            }
            else if (a.z < b.z)
            {
                a.vz++;
                b.vz--;
            }
        }

    }
    public class Moon
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int vx { get; set; }
        public int vy { get; set; }
        public int vz { get; set; }
        public int pot => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        public int kin => Math.Abs(vx) + Math.Abs(vy) + Math.Abs(vz);
        public int energy => kin * pot;
        public void ApplyVelocity()
        {
            x += vx;
            y += vy;
            z += vz;
        }
    }

}
