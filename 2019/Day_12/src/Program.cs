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

            Console.WriteLine(Task1(input, 1000));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(List<Moon> moons, long steps = 1000)
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
            string xPrev = null;
            var xSet = new HashSet<string>();

            bool yComplete = false;
            string yPrev = null;
            var ySet = new HashSet<string>();

            bool zComplete = false;
            string zPrev = null;
            var zSet = new HashSet<string>();

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
                    xPrev = $"{moons[0].x} {moons[0].vx} {moons[1].x} {moons[1].vx} {moons[2].x} {moons[2].vx} {moons[3].x} {moons[3].vx}";
                    if (xSet.Contains(xPrev))
                    {
                        xComplete = true;
                    }
                    xSet.Add(xPrev);
                }
                if (!yComplete)
                {
                    yPrev = $"{moons[0].y} {moons[0].vy} {moons[1].y} {moons[1].vy} {moons[2].y} {moons[2].vy} {moons[3].y} {moons[3].vy}";
                    if (ySet.Contains(yPrev))
                    {
                        yComplete = true;
                    }
                    ySet.Add(yPrev);
                }
                if (!zComplete)
                {
                    zPrev = $"{moons[0].z} {moons[0].vz} {moons[1].z} {moons[1].vz} {moons[2].z} {moons[2].vz} {moons[3].z} {moons[3].vz}";
                    if (zSet.Contains(zPrev))
                    {
                        zComplete = true;
                    }
                    zSet.Add(zPrev);
                }
            }

            return Lcm(xSet.Count, Lcm(ySet.Count, zSet.Count)).ToString();
        }

        public static long Lcm(long a, long b) => a * b / Gcd(a, b);
        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

        public static void CalculatePosition(List<Moon> moons, long steps)
        {
            var pairs = new (int, int)[]{
                (0,1),
                (0,2),
                (0,3),
                (1,2),
                (1,3),
                (2,3)
            };

            for (long i = 0; i < steps; i++)
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
        public long x { get; set; }
        public long y { get; set; }
        public long z { get; set; }
        public long vx { get; set; }
        public long vy { get; set; }
        public long vz { get; set; }
        public long pot => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        public long kin => Math.Abs(vx) + Math.Abs(vy) + Math.Abs(vz);
        public long energy => kin * pot;
        public void ApplyVelocity()
        {
            x += vx;
            y += vy;
            z += vz;
        }
    }
}