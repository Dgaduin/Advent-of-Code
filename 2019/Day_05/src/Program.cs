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
            Task1(input, 5);
        }

        public static int[] Task1(int[] instructions, int input = 1)
        {
            var output = new List<int>(100);
            int pointer = 0;
            while (true)
            {
                var instruction = instructions[pointer];
                var opcode = instruction % 100;
                if (opcode == 99) break;
                else if (opcode == 1)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    instructions[instructions[pointer + 3]] = a + b;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 2)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    instructions[instructions[pointer + 3]] = a * b;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 5)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    if (a != 0)
                    {
                        pointer = b;
                        Console.WriteLine(b);
                    }
                    else
                        pointer += 3;
                    continue;
                }
                else if (opcode == 6)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    if (a == 0)
                    {
                        pointer = b;
                        Console.WriteLine(b);
                    }
                    else
                        pointer += 3;
                    continue;
                }
                else if (opcode == 7)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    var c = a < b ? 1 : 0;

                    instructions[instructions[pointer + 3]] = c;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    var a = (instruction / 100) % 10 == 0
                        ? instructions[instructions[pointer + 1]]
                        : instructions[pointer + 1];

                    var b = (instruction / 1000) % 10 == 0
                        ? instructions[instructions[pointer + 2]]
                        : instructions[pointer + 2];

                    var c = a == b ? 1 : 0;

                    instructions[instructions[pointer + 3]] = c;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 3)
                {
                    instructions[instructions[pointer + 1]] = input;
                    pointer += 2;
                    continue;
                }
                else if (opcode == 4)
                {
                    output.Add(instructions[instructions[pointer + 1]]);
                    pointer += 2;
                    continue;
                }
            }

            output.ForEach(Console.WriteLine);

            return instructions;
        }

    }
}
