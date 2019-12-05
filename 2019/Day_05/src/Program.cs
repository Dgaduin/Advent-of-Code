using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_05
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] input = File
                .ReadAllText("input.txt")
                .Split(',')
                .Select(x => Int32.Parse(x))
                .ToArray();

            Task1(input);
            //Console.WriteLine(Task2(input));
        }

        public static int[] Task1(int[] instructions, int input = 1)
        {
            var output = new List<int>(100);
            for (int i = 0; i < instructions.Length;)
            {
                var instruction = instructions[i];
                var opcode = instruction % 100;
                if (opcode == 99) break;
                if (opcode == 1)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[i + 1]]
                        : instructions[i + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[i + 2]]
                        : instructions[i + 2];

                    instructions[instructions[i + 3]] = a + b;
                    i += 4;
                    continue;
                }
                if (opcode == 2)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[i + 1]]
                        : instructions[i + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[i + 2]]
                        : instructions[i + 2];

                    instructions[instructions[i + 3]] = a * b;
                    i += 4;
                    continue;
                }
                if (opcode == 3)
                {
                    instructions[instructions[i + 1]] = input;
                    i += 2;
                    continue;
                }
                if (opcode == 4)
                {
                    output.Add(instructions[instructions[i + 1]]);
                    i += 2;
                    continue;
                }
            }

            output.ForEach(Console.WriteLine);

            return instructions;
        }
        public static string Task2() { return ""; }
    }
}
