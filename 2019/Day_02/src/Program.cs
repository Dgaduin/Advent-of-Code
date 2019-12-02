using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_02
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

            var task1input = (int[])input.Clone();
            task1input[1] = 12;
            task1input[2] = 2;
            Console.WriteLine(Task1(task1input)[0]);
            Console.WriteLine(Task2(input));
        }

        public static int[] Task1(int[] opcodes)
        {
            for (int i = 0; i < opcodes.Length; i += 4)
            {
                var command = opcodes[i];
                if (command == 99) break;
                if (command == 1) opcodes[opcodes[i + 3]] = opcodes[opcodes[i + 1]] + opcodes[opcodes[i + 2]];
                if (command == 2) opcodes[opcodes[i + 3]] = opcodes[opcodes[i + 1]] * opcodes[opcodes[i + 2]];
            }
            return opcodes;
        }
        public static int Task2(int[] opcodes)
        {
            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    var opCodeCopy = (int[])opcodes.Clone();
                    opCodeCopy[1] = i;
                    opCodeCopy[2] = j;
                    var temp = Task1(opCodeCopy);
                    if (temp[0] == 19690720) return i * 100 + j;
                }
            }
            return -1;
        }
    }
}
