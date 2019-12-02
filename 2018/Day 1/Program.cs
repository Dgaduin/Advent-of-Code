using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable input = File.ReadLines("input.txt");

            // Task 1
            int returnValueTask1 = 0;
            foreach (string value in input)
            {
                int numberValue = Int32.Parse(value);
                returnValueTask1 += numberValue;
            }
            Console.WriteLine(returnValueTask1);

            // Task 2
            bool stopFlag = true;
            int returnValueTask2 = 0;
            HashSet<int> foundNumbers = new HashSet<int>();
            foundNumbers.Add(0);
            while (stopFlag)
            {
                foreach (string value in input)
                {
                    int numberValue = Int32.Parse(value);
                    returnValueTask2 += numberValue;
                    if (foundNumbers.Contains(returnValueTask2))
                    {
                        Console.WriteLine(returnValueTask2);
                        stopFlag = false;
                        break;
                    }
                    else
                        foundNumbers.Add(returnValueTask2);
                }
            }
        }
    }
}
