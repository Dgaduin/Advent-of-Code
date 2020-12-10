using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_08
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

        public static int Task1(List<string> input)
        {
            int acc = 0;
            var set = new HashSet<int>();
            var parsedList = input.Select(x =>
            {
                var t = x.Split(' ');
                return new { opp = t[0], value = int.Parse(t[1]) };
            }).ToList();

            for (int i = 0; ;)
            {
                if (set.Contains(i)) return acc;
                set.Add(i);
                if (parsedList[i].opp == "acc") { acc += parsedList[i].value; i++; }
                else if (parsedList[i].opp == "jmp") i += parsedList[i].value;
                else i++;
            }
            return 0;
        }
        public static int Task2(List<string> input)
        {
            var parsedList = input.Select(x =>
            {
                var t = x.Split(' ');
                return new Opp { opp = t[0], value = int.Parse(t[1]) };
            }).ToList();

            var nonAcc = parsedList.Select((x, i) => x.opp == "acc" ? -1 : i).Where(y => y != -1).ToList();
            int retValue = 0;
            foreach (var nonAccIndex in nonAcc)
            {
                var parsedListCopy = parsedList.Select(x => new Opp { opp = x.opp, value = x.value }).ToList();

                var swappedItem = parsedListCopy[nonAccIndex];

                if (swappedItem.opp == "nop") swappedItem.opp = "jmp";
                else swappedItem.opp = "nop";

                var breakFlag = false;
                int acc = 0;
                var set = new HashSet<int>();
                for (int i = 0; i < parsedListCopy.Count;)
                {
                    var item = parsedListCopy[i];
                    if (set.Contains(i))
                    {
                        breakFlag = true;
                        break;
                    }
                    set.Add(i);
                    if (item.opp == "acc") { acc += item.value; i++; }
                    else if (item.opp == "jmp") i += item.value;
                    else i++;
                }
                if (breakFlag)
                {
                    continue;
                }
                else
                {
                    retValue = acc;
                    break;
                }
            }

            return retValue;
        }

        private class Opp
        {
            public string opp { get; set; }
            public int value { get; set; }
        }
    }
}
