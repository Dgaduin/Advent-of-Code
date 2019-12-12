using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Day_11
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            long[] input =
                File.ReadAllText("input.txt")
                .Split(',')
                .Select(x => Int64.Parse(x))
                .ToArray();

            var instrunctions = new long[50000];
            input.CopyTo(instrunctions, 0);

            Console.WriteLine(await Task1(instrunctions));
            Console.WriteLine(await Task2(instrunctions));
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
                    output.Complete();
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
                else if (opcode == 9)
                {
                    ParamIndexes(instruction, out var param1, out var param2, out var param3);

                    offset += instructions[param1];
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
                    2 => instructions[pointer + 1] + offset,
                    _ => 0
                };
                param2 = ((instruction / 1000) % 10) switch
                {
                    0 => instructions[pointer + 2],
                    1 => pointer + 2,
                    2 => instructions[pointer + 2] + offset,
                    _ => 0
                };
                param3 = ((instruction / 10000) % 10) switch
                {
                    0 => instructions[pointer + 3],
                    1 => pointer + 3,
                    2 => instructions[pointer + 3] + offset,
                    _ => 0
                };
            }
        }

        public static async Task NavCode(BufferBlock<long> input, BufferBlock<long> output, Dictionary<(int, int), long> map)
        {
            int direction = 1;
            var current = (0, 0);
            while (await input.OutputAvailableAsync())
            {
                var colour = input.Receive();
                await input.OutputAvailableAsync();
                var rotate = input.Receive();

                if (map.ContainsKey(current))
                    map[current] = colour;
                else
                    map.Add(current, colour);

                if (rotate == 0)
                {
                    switch (direction)
                    {
                        case 1:
                            {
                                direction = 4;
                                current = (--current.Item1, current.Item2);
                                break;
                            }
                        case 2:
                            {
                                direction = 1;
                                current = (current.Item1, --current.Item2);
                                break;
                            }
                        case 3:
                            {
                                direction = 2;
                                current = (++current.Item1, current.Item2);
                                break;
                            }
                        case 4:
                            {
                                direction = 3;
                                current = (current.Item1, ++current.Item2);
                                break;
                            }

                    }
                }
                if (rotate == 1)
                {
                    switch (direction)
                    {
                        case 1:
                            {
                                direction = 2;
                                current = (++current.Item1, current.Item2);
                                break;
                            }
                        case 2:
                            {
                                direction = 3;
                                current = (current.Item1, ++current.Item2);
                                break;
                            }
                        case 3:
                            {
                                direction = 4;
                                current = (--current.Item1, current.Item2);
                                break;
                            }
                        case 4:
                            {
                                direction = 1;
                                current = (current.Item1, --current.Item2);
                                break;
                            }

                    }
                }

                if (map.ContainsKey(current))
                    output.Post(map[current]);
                else
                    output.Post(0);
            }
        }

        public static async Task<string> Task1(long[] instructions)
        {
            var map = new Dictionary<(int, int), long>();

            var input1 = new BufferBlock<long>();
            input1.Post(0);
            var output1 = new BufferBlock<long>();
            Task.WaitAll(
                InitCode(instructions, input1, output1),
                NavCode(output1, input1, map)
                );

            return map.Count().ToString();
        }

        public static async Task<string> Task2(long[] instructions)
        {
            var map = new Dictionary<(int, int), long>();

            var input1 = new BufferBlock<long>();
            input1.Post(1);
            var output1 = new BufferBlock<long>();
            Task.WaitAll(
                InitCode(instructions, input1, output1),
                NavCode(output1, input1, map)
                );

            int xOffset = map.Min(z => z.Key.Item1);
            int yOffset = map.Min(z => z.Key.Item2);

            var height = map.Max(z => z.Key.Item2);
            var width = map.Max(z => z.Key.Item1);

            var sb = new StringBuilder();


            for (int j = yOffset; j <= height; j++)
            {
                for (int i = xOffset; i <= width; i++)
                {
                    if (map.TryGetValue((i, j), out var colour))
                    {
                        if (colour == 1) sb.Append('*');
                        else sb.Append(' ');
                    }
                    else sb.Append(' ');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
