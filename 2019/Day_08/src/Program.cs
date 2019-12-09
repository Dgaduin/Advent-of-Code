using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day_08
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            var layers = Enumerable.Range(0, input.Length / 150)
                    .Select(i => input.Substring(i * 150, 150));


            Console.WriteLine(Task1(layers));
            Console.WriteLine(Task2(layers));
        }

        public static string Task1(IEnumerable<string> input)
        {
            string min0Layer = null;
            int min = Int32.MaxValue;
            foreach (var layer in input)
            {
                var count = layer.Count(x => x == '0');
                if (count < min)
                {
                    min = count;
                    min0Layer = layer;
                }
            }
            int count1 = min0Layer.Count(x => x == '1');
            int count2 = min0Layer.Count(x => x == '2');
            return $"{count1 * count2}";
        }
        public static string Task2(IEnumerable<string> input)
        {
            var current = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000".ToList();
            foreach (var layer in input.Reverse())
            {
                for (int i = 0; i < layer.Length; i++)
                {
                    if (layer[i] != '2')
                    {
                        current[i] = layer[i];
                    }
                }
            }

            var sb = new StringBuilder();
            sb.Append(current.Take(25).ToArray());
            sb.AppendLine();
            sb.Append(current.Skip(25).Take(25).ToArray());
            sb.AppendLine();
            sb.Append(current.Skip(50).Take(25).ToArray());
            sb.AppendLine();
            sb.Append(current.Skip(75).Take(25).ToArray());
            sb.AppendLine();
            sb.Append(current.Skip(100).Take(25).ToArray());
            sb.AppendLine();
            sb.Append(current.Skip(125).Take(25).ToArray());
            return sb.ToString();
        }
    }
}
