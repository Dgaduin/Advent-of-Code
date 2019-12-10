using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Day_07
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            long[] input = File
                .ReadAllText("input.txt")
                .Split(',')
                .Select(x => Int64.Parse(x))
                .ToArray();

            Console.WriteLine(await Task1(input));
            Console.WriteLine(await Task2(input));
        }

        public static async Task InitCode(long[] instructions, BufferBlock<long> input, BufferBlock<long> output, bool log = false)
        {
            long pointer = 0;
            long offset = 0;
            while (true)
            {
                var instruction = instructions[pointer];
                var opcode = instruction % 100;
                if (opcode == 99)
                {
                    return;
                }
                else if (opcode == 1)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    instructions[param3] = instructions[param1] + instructions[param2];
                    pointer += 4;
                    continue;
                }
                else if (opcode == 2)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    instructions[param3] = instructions[param1] * instructions[param2];
                    pointer += 4;
                    continue;
                }
                else if (opcode == 5)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    if (instructions[param1] != 0)
                    {
                        pointer = instructions[param2];
                    }
                    else
                        pointer += 3;
                    continue;
                }
                else if (opcode == 6)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    if (instructions[param1] == 0)
                    {
                        pointer = instructions[param2];
                    }
                    else
                        pointer += 3;
                    continue;
                }
                else if (opcode == 7)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    var c = instructions[param1] < instructions[param2] ? 1 : 0;

                    instructions[param3] = c;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    var c = instructions[param1] == instructions[param2] ? 1 : 0;

                    instructions[param3] = c;
                    pointer += 4;
                    continue;
                }
                else if (opcode == 3)
                {
                    var a = await input.ReceiveAsync();
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    instructions[param1] = a;
                    pointer += 2;
                    continue;
                }
                else if (opcode == 4)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    await output.SendAsync(instructions[param1]);
                    pointer += 2;
                    continue;
                }
            }

            void ParamIndexes(long instruction, out long param1, out long param2, out long param3)
            {
                param1 = ((instruction / 100) % 10) switch
                {
                    0 => instructions[pointer + 1],
                    1 => pointer + 1,
                    2 => instructions[pointer + 1 + offset]
                };
                param2 = ((instruction / 1000) % 10) switch
                {
                    0 => instructions[pointer + 2],
                    1 => pointer + 2,
                    2 => instructions[pointer + 2 + offset]
                };
                param3 = ((instruction / 10000) % 10) switch
                {
                    0 => instructions[pointer + 3],
                    1 => pointer + 3,
                    2 => instructions[pointer + 3 + offset]
                };
            }
        }
        public static async Task<string> Task1(long[] instructions)
        {
            long max = 0;
            foreach (var perm in Task1Perms)
            {
                var input1 = new BufferBlock<long>();
                input1.Post(perm[0]);
                input1.Post(0);
                var output1 = new BufferBlock<long>();
                output1.Post(perm[1]);
                var output2 = new BufferBlock<long>();
                output2.Post(perm[2]);
                var output3 = new BufferBlock<long>();
                output3.Post(perm[3]);
                var output4 = new BufferBlock<long>();
                output4.Post(perm[4]);
                var output5 = new BufferBlock<long>();
                InitCode(instructions, input1, output1);
                InitCode(instructions, output1, output2);
                InitCode(instructions, output2, output3);
                InitCode(instructions, output3, output4);
                InitCode(instructions, output4, output5);
                var thrust = await output5.ReceiveAsync();
                if (thrust > max) max = thrust;
            }
            return max.ToString();
        }

        public static async Task<string> Task2(long[] instructions)
        {
            long max = 0;
            foreach (var perm in Task2Perms)
            {
                var input1 = new BufferBlock<long>();
                input1.Post(perm[0]);
                input1.Post(0);
                var output1 = new BufferBlock<long>();
                output1.Post(perm[1]);
                var output2 = new BufferBlock<long>();
                output2.Post(perm[2]);
                var output3 = new BufferBlock<long>();
                output3.Post(perm[3]);
                var output4 = new BufferBlock<long>();
                output4.Post(perm[4]);

                await Task.WhenAll(
                    InitCode(instructions, input1, output1),
                    InitCode(instructions, output1, output2),
                    InitCode(instructions, output2, output3),
                    InitCode(instructions, output3, output4),
                    InitCode(instructions, output4, input1));

                var thrust = input1.Receive();
                if (thrust > max) max = thrust;
            }

            return max.ToString();
        }

        public static long[][] Task1Perms = new long[][]
        {
            new long[] { 0, 1, 2, 3, 4 },
            new long[] { 1, 0, 2, 3, 4 },
            new long[] { 2, 0, 1, 3, 4 },
            new long[] { 0, 2, 1, 3, 4 },
            new long[] { 1, 2, 0, 3, 4 },
            new long[] { 2, 1, 0, 3, 4 },
            new long[] { 2, 1, 3, 0, 4 },
            new long[] { 1, 2, 3, 0, 4 },
            new long[] { 3, 2, 1, 0, 4 },
            new long[] { 2, 3, 1, 0, 4 },
            new long[] { 1, 3, 2, 0, 4 },
            new long[] { 3, 1, 2, 0, 4 },
            new long[] { 3, 0, 2, 1, 4 },
            new long[] { 0, 3, 2, 1, 4 },
            new long[] { 2, 3, 0, 1, 4 },
            new long[] { 3, 2, 0, 1, 4 },
            new long[] { 0, 2, 3, 1, 4 },
            new long[] { 2, 0, 3, 1, 4 },
            new long[] { 1, 0, 3, 2, 4 },
            new long[] { 0, 1, 3, 2, 4 },
            new long[] { 3, 1, 0, 2, 4 },
            new long[] { 1, 3, 0, 2, 4 },
            new long[] { 0, 3, 1, 2, 4 },
            new long[] { 3, 0, 1, 2, 4 },
            new long[] { 4, 0, 1, 2, 3 },
            new long[] { 0, 4, 1, 2, 3 },
            new long[] { 1, 4, 0, 2, 3 },
            new long[] { 4, 1, 0, 2, 3 },
            new long[] { 0, 1, 4, 2, 3 },
            new long[] { 1, 0, 4, 2, 3 },
            new long[] { 1, 0, 2, 4, 3 },
            new long[] { 0, 1, 2, 4, 3 },
            new long[] { 2, 1, 0, 4, 3 },
            new long[] { 1, 2, 0, 4, 3 },
            new long[] { 0, 2, 1, 4, 3 },
            new long[] { 2, 0, 1, 4, 3 },
            new long[] { 2, 4, 1, 0, 3 },
            new long[] { 4, 2, 1, 0, 3 },
            new long[] { 1, 2, 4, 0, 3 },
            new long[] { 2, 1, 4, 0, 3 },
            new long[] { 4, 1, 2, 0, 3 },
            new long[] { 1, 4, 2, 0, 3 },
            new long[] { 0, 4, 2, 1, 3 },
            new long[] { 4, 0, 2, 1, 3 },
            new long[] { 2, 0, 4, 1, 3 },
            new long[] { 0, 2, 4, 1, 3 },
            new long[] { 4, 2, 0, 1, 3 },
            new long[] { 2, 4, 0, 1, 3 },
            new long[] { 3, 4, 0, 1, 2 },
            new long[] { 4, 3, 0, 1, 2 },
            new long[] { 0, 3, 4, 1, 2 },
            new long[] { 3, 0, 4, 1, 2 },
            new long[] { 4, 0, 3, 1, 2 },
            new long[] { 0, 4, 3, 1, 2 },
            new long[] { 0, 4, 1, 3, 2 },
            new long[] { 4, 0, 1, 3, 2 },
            new long[] { 1, 0, 4, 3, 2 },
            new long[] { 0, 1, 4, 3, 2 },
            new long[] { 4, 1, 0, 3, 2 },
            new long[] { 1, 4, 0, 3, 2 },
            new long[] { 1, 3, 0, 4, 2 },
            new long[] { 3, 1, 0, 4, 2 },
            new long[] { 0, 1, 3, 4, 2 },
            new long[] { 1, 0, 3, 4, 2 },
            new long[] { 3, 0, 1, 4, 2 },
            new long[] { 0, 3, 1, 4, 2 },
            new long[] { 4, 3, 1, 0, 2 },
            new long[] { 3, 4, 1, 0, 2 },
            new long[] { 1, 4, 3, 0, 2 },
            new long[] { 4, 1, 3, 0, 2 },
            new long[] { 3, 1, 4, 0, 2 },
            new long[] { 1, 3, 4, 0, 2 },
            new long[] { 2, 3, 4, 0, 1 },
            new long[] { 3, 2, 4, 0, 1 },
            new long[] { 4, 2, 3, 0, 1 },
            new long[] { 2, 4, 3, 0, 1 },
            new long[] { 3, 4, 2, 0, 1 },
            new long[] { 4, 3, 2, 0, 1 },
            new long[] { 4, 3, 0, 2, 1 },
            new long[] { 3, 4, 0, 2, 1 },
            new long[] { 0, 4, 3, 2, 1 },
            new long[] { 4, 0, 3, 2, 1 },
            new long[] { 3, 0, 4, 2, 1 },
            new long[] { 0, 3, 4, 2, 1 },
            new long[] { 0, 2, 4, 3, 1 },
            new long[] { 2, 0, 4, 3, 1 },
            new long[] { 4, 0, 2, 3, 1 },
            new long[] { 0, 4, 2, 3, 1 },
            new long[] { 2, 4, 0, 3, 1 },
            new long[] { 4, 2, 0, 3, 1 },
            new long[] { 3, 2, 0, 4, 1 },
            new long[] { 2, 3, 0, 4, 1 },
            new long[] { 0, 3, 2, 4, 1 },
            new long[] { 3, 0, 2, 4, 1 },
            new long[] { 2, 0, 3, 4, 1 },
            new long[] { 0, 2, 3, 4, 1 },
            new long[] { 1, 2, 3, 4, 0 },
            new long[] { 2, 1, 3, 4, 0 },
            new long[] { 3, 1, 2, 4, 0 },
            new long[] { 1, 3, 2, 4, 0 },
            new long[] { 2, 3, 1, 4, 0 },
            new long[] { 3, 2, 1, 4, 0 },
            new long[] { 3, 2, 4, 1, 0 },
            new long[] { 2, 3, 4, 1, 0 },
            new long[] { 4, 3, 2, 1, 0 },
            new long[] { 3, 4, 2, 1, 0 },
            new long[] { 2, 4, 3, 1, 0 },
            new long[] { 4, 2, 3, 1, 0 },
            new long[] { 4, 1, 3, 2, 0 },
            new long[] { 1, 4, 3, 2, 0 },
            new long[] { 3, 4, 1, 2, 0 },
            new long[] { 4, 3, 1, 2, 0 },
            new long[] { 1, 3, 4, 2, 0 },
            new long[] { 3, 1, 4, 2, 0 },
            new long[] { 2, 1, 4, 3, 0 },
            new long[] { 1, 2, 4, 3, 0 },
            new long[] { 4, 2, 1, 3, 0 },
            new long[] { 2, 4, 1, 3, 0 },
            new long[] { 1, 4, 2, 3, 0 },
            new long[] { 4, 1, 2, 3, 0 }
        };

        public static long[][] Task2Perms = new long[][]
        {
            new long[]{5,6,7,8,9},
            new long[]{6,5,7,8,9},
            new long[]{7,5,6,8,9},
            new long[]{5,7,6,8,9},
            new long[]{6,7,5,8,9},
            new long[]{7,6,5,8,9},
            new long[]{7,6,8,5,9},
            new long[]{6,7,8,5,9},
            new long[]{8,7,6,5,9},
            new long[]{7,8,6,5,9},
            new long[]{6,8,7,5,9},
            new long[]{8,6,7,5,9},
            new long[]{8,5,7,6,9},
            new long[]{5,8,7,6,9},
            new long[]{7,8,5,6,9},
            new long[]{8,7,5,6,9},
            new long[]{5,7,8,6,9},
            new long[]{7,5,8,6,9},
            new long[]{6,5,8,7,9},
            new long[]{5,6,8,7,9},
            new long[]{8,6,5,7,9},
            new long[]{6,8,5,7,9},
            new long[]{5,8,6,7,9},
            new long[]{8,5,6,7,9},
            new long[]{9,5,6,7,8},
            new long[]{5,9,6,7,8},
            new long[]{6,9,5,7,8},
            new long[]{9,6,5,7,8},
            new long[]{5,6,9,7,8},
            new long[]{6,5,9,7,8},
            new long[]{6,5,7,9,8},
            new long[]{5,6,7,9,8},
            new long[]{7,6,5,9,8},
            new long[]{6,7,5,9,8},
            new long[]{5,7,6,9,8},
            new long[]{7,5,6,9,8},
            new long[]{7,9,6,5,8},
            new long[]{9,7,6,5,8},
            new long[]{6,7,9,5,8},
            new long[]{7,6,9,5,8},
            new long[]{9,6,7,5,8},
            new long[]{6,9,7,5,8},
            new long[]{5,9,7,6,8},
            new long[]{9,5,7,6,8},
            new long[]{7,5,9,6,8},
            new long[]{5,7,9,6,8},
            new long[]{9,7,5,6,8},
            new long[]{7,9,5,6,8},
            new long[]{8,9,5,6,7},
            new long[]{9,8,5,6,7},
            new long[]{5,8,9,6,7},
            new long[]{8,5,9,6,7},
            new long[]{9,5,8,6,7},
            new long[]{5,9,8,6,7},
            new long[]{5,9,6,8,7},
            new long[]{9,5,6,8,7},
            new long[]{6,5,9,8,7},
            new long[]{5,6,9,8,7},
            new long[]{9,6,5,8,7},
            new long[]{6,9,5,8,7},
            new long[]{6,8,5,9,7},
            new long[]{8,6,5,9,7},
            new long[]{5,6,8,9,7},
            new long[]{6,5,8,9,7},
            new long[]{8,5,6,9,7},
            new long[]{5,8,6,9,7},
            new long[]{9,8,6,5,7},
            new long[]{8,9,6,5,7},
            new long[]{6,9,8,5,7},
            new long[]{9,6,8,5,7},
            new long[]{8,6,9,5,7},
            new long[]{6,8,9,5,7},
            new long[]{7,8,9,5,6},
            new long[]{8,7,9,5,6},
            new long[]{9,7,8,5,6},
            new long[]{7,9,8,5,6},
            new long[]{8,9,7,5,6},
            new long[]{9,8,7,5,6},
            new long[]{9,8,5,7,6},
            new long[]{8,9,5,7,6},
            new long[]{5,9,8,7,6},
            new long[]{9,5,8,7,6},
            new long[]{8,5,9,7,6},
            new long[]{5,8,9,7,6},
            new long[]{5,7,9,8,6},
            new long[]{7,5,9,8,6},
            new long[]{9,5,7,8,6},
            new long[]{5,9,7,8,6},
            new long[]{7,9,5,8,6},
            new long[]{9,7,5,8,6},
            new long[]{8,7,5,9,6},
            new long[]{7,8,5,9,6},
            new long[]{5,8,7,9,6},
            new long[]{8,5,7,9,6},
            new long[]{7,5,8,9,6},
            new long[]{5,7,8,9,6},
            new long[]{6,7,8,9,5},
            new long[]{7,6,8,9,5},
            new long[]{8,6,7,9,5},
            new long[]{6,8,7,9,5},
            new long[]{7,8,6,9,5},
            new long[]{8,7,6,9,5},
            new long[]{8,7,9,6,5},
            new long[]{7,8,9,6,5},
            new long[]{9,8,7,6,5},
            new long[]{8,9,7,6,5},
            new long[]{7,9,8,6,5},
            new long[]{9,7,8,6,5},
            new long[]{9,6,8,7,5},
            new long[]{6,9,8,7,5},
            new long[]{8,9,6,7,5},
            new long[]{9,8,6,7,5},
            new long[]{6,8,9,7,5},
            new long[]{8,6,9,7,5},
            new long[]{7,6,9,8,5},
            new long[]{6,7,9,8,5},
            new long[]{9,7,6,8,5},
            new long[]{7,9,6,8,5},
            new long[]{6,9,7,8,5},
            new long[]{9,6,7,8,5}
        };
    }
}
