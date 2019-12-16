using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Day_13
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            long[] input =
                File.ReadAllText("input.txt")
                .Split(',')
                .Select(x => Int64.Parse(x))
                .ToArray();

            var instrunctions = new long[50000];
            input.CopyTo(instrunctions, 0);

            Console.WriteLine(await Task1(instrunctions));
            Console.WriteLine(Task2());
        }

        public static async Task<string> Task1(long[] instructions)
        {
            var input1 = new BufferBlock<long>();
            input1.Post(0);
            var output1 = new BufferBlock<long>();
            InitCode(instructions, input1, output1);
            var result = await DrawScreen(output1);
            return result.Count(z => z.Value == 2).ToString();
        }
        public static string Task2() { return ""; }

        public static async Task<Dictionary<(long, long), long>> Hack(BufferBlock<long> input)
        {
            var screen = new Dictionary<(long, long), long>();

            while (await input.OutputAvailableAsync())
            {
                var x = input.Receive();
                await input.OutputAvailableAsync();
                var y = input.Receive();
                await input.OutputAvailableAsync();
                var item = input.Receive();

                if (screen.ContainsKey((x, y)))
                    screen[(x, y)] = item;
                else
                    screen.Add((x, y), item);

            }

            return screen;
        }

        public static async Task<Dictionary<(long, long), long>> DrawScreen(BufferBlock<long> input)
        {
            var screen = new Dictionary<(long, long), long>();

            while (await input.OutputAvailableAsync())
            {
                var x = input.Receive();
                await input.OutputAvailableAsync();
                var y = input.Receive();
                await input.OutputAvailableAsync();
                var item = input.Receive();

                if (screen.ContainsKey((x, y)))
                    screen[(x, y)] = item;
                else
                    screen.Add((x, y), item);
            }

            return screen;
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
    }
}
