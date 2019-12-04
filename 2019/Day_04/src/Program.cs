using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_04
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(string input)
        {
            var start = Int32.Parse(input.Substring(0, 6));
            var end = Int32.Parse(input.Substring(7, 6));
            int count = 0;
            for (int i = start; i <= end; i++)
            {
                if (IsPasswordValid1(i.ToString())) count++;
            }
            return count.ToString();
        }
        public static string Task2(string input)
        {
            var start = Int32.Parse(input.Substring(0, 6));
            var end = Int32.Parse(input.Substring(7, 6));
            int count = 0;
            for (int i = start; i <= end; i++)
            {
                if (IsPasswordValid2(i.ToString())) count++;
            }
            return count.ToString();
        }

        public static bool IsPasswordValid1(string password)
        {
            var regex = new Regex("(.)\\1");
            if (!regex.IsMatch(password))
                return false;

            for (int i = 1; i < password.Length; i++)
            {
                if (password[i] < password[i - 1]) return false;
            }

            return true;
        }

        public static bool IsPasswordValid2(string password)
        {
            var regex = new Regex("(\\d)\\1+");
            var exactly2Match = regex.Matches(password).Cast<Match>().Where(x => x.Length == 2).Count();
            if (exactly2Match == 0)
                return false;

            for (int i = 1; i < password.Length; i++)
            {
                if (password[i] < password[i - 1]) return false;
            }

            return true;
        }
    }
}
