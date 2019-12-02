using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> input = File.ReadLines("input.txt").ToList();

            // Task 1
            int twos = 0;
            int threes = 0;
            foreach (string value in input)
            {
                var numberBreakdown = value.GroupBy(x => x).Select(x => x.Count());
                if (numberBreakdown.Any(x => x == 2))
                    twos++;
                if (numberBreakdown.Any(x => x == 3))
                    threes++;
            }
            Console.WriteLine(twos * threes);

            // Task 2
            Func<string, string, Tuple<bool, int>> compare = (string a, string b) =>
               {
                   int missmatchNumber = 0;
                   int foundIndex = -1;

                   for (int i = 0; i < a.Length; i++)
                   {
                       if (a[i] != b[i])
                       {
                           missmatchNumber++;
                           foundIndex = i;
                       }
                       if (missmatchNumber > 1)
                       {
                           return Tuple.Create(false, -1);
                       }
                   }
                   return Tuple.Create(missmatchNumber == 1, foundIndex);
               };
            for (int i = 0; i < input.Count() - 1; i++)
            {
                for (int j = i + 1; j < input.Count(); j++)
                {
                    var compareResult = compare(input[i], input[j]);
                    if (compareResult.Item1)
                    {
                        var sb = new StringBuilder(input[i]).Remove(compareResult.Item2,1);
                        Console.WriteLine(sb.ToString());
                        return;
                    }
                }
            }
        }
    }
}
