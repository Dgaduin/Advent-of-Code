using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Day_04
{
    public class Program
    {
        static void Main(string[] args)
        {
            // List<string> input = File.ReadLines("input.txt").ToList();
            string input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static string Task1(string input)
        {
            int trailingNumber = 0;
            for (int i = 0; ; i++)
            {
                var s = CreateMD5($"{input}{i}");
                if (s.StartsWith("00000"))
                {
                    trailingNumber = i;
                    break;
                }
            }
            return trailingNumber.ToString();
        }
        public static string Task2(string input)
        {
            int trailingNumber = 0;
            for (int i = 0; ; i++)
            {
                var s = CreateMD5($"{input}{i}");
                if (s.StartsWith("000000"))
                {
                    trailingNumber = i;
                    break;
                }
            }
            return trailingNumber.ToString();
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
