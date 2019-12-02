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
            // string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(List<string> input)
        {
            bool[,] lights = new bool[1000, 1000];

            var r1 = new Regex("(toggle|on|off) (\\d{1,}),(\\d{1,}).+ (\\d{1,}),(\\d{1,})");
            foreach (string s in input)
            {
                var match = r1.Match(s).Groups;
                var type = match[1].Value switch
                {
                    "toggle" => 1,
                    "on" => 2,
                    "off" => 3,
                    _ => 0
                };

                for (int i = Int32.Parse(match[2].Value); i <= Int32.Parse(match[4].Value); i++)
                {
                    for (int j = Int32.Parse(match[3].Value); j <= Int32.Parse(match[5].Value); j++)
                    {
                        switch (type)
                        {
                            case 1:
                                lights[i, j] = !lights[i, j];
                                break;
                            case 2:
                                lights[i, j] = true;
                                break;
                            case 3:
                                lights[i, j] = false;
                                break;
                        }
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (lights[i, j]) count++;
                }
            }

            return count.ToString();
        }
        public static string Task2(List<string> input)
        {
            int[,] lights = new int[1000, 1000];

            var r1 = new Regex("(toggle|on|off) (\\d{1,}),(\\d{1,}).+ (\\d{1,}),(\\d{1,})");
            foreach (string s in input)
            {
                var match = r1.Match(s).Groups;
                var type = match[1].Value switch
                {
                    "toggle" => 1,
                    "on" => 2,
                    "off" => 3,
                    _ => 0
                };

                for (int i = Int32.Parse(match[2].Value); i <= Int32.Parse(match[4].Value); i++)
                {
                    for (int j = Int32.Parse(match[3].Value); j <= Int32.Parse(match[5].Value); j++)
                    {
                        switch (type)
                        {
                            case 1:
                                lights[i, j] += 2;
                                break;
                            case 2:
                                lights[i, j]++;
                                break;
                            case 3:
                                lights[i, j]--;
                                if (lights[i, j] < 0)
                                    lights[i, j] = 0;
                                break;
                        }
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    count = count + lights[i, j];
                }
            }

            return count.ToString();
        }
    }
}
