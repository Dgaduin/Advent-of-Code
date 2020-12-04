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
            var input = File.ReadAllText("input.txt");

            Console.WriteLine(Task1(input));
            Console.WriteLine(Task2(input));
        }

        public static int Task1(string input)
        {
            var t = ParseInput(input);
            return t.Count(x =>
                x.Keys.Contains("byr") &&
                x.Keys.Contains("iyr") &&
                x.Keys.Contains("eyr") &&
                x.Keys.Contains("hgt") &&
                x.Keys.Contains("hcl") &&
                x.Keys.Contains("ecl") &&
                x.Keys.Contains("pid")
            );
        }
        public static int Task2(string input)
        {
            var t = ParseInput(input);
            return t.Count(x =>
                ValidByr(x) &&
                ValidIyr(x) &&
                ValidEyr(x) &&
                ValidHgt(x) &&
                ValidEcl(x) &&
                ValidPid(x) &&
                ValidHcl(x)
            );
        }

        public static List<Dictionary<string, string>> ParseInput(string input) =>

            input.Split("\n\r")
                   .Select(x =>
                      x.Replace('\r', ' ')
                       .Replace('\n', ' ')
                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                       .ToDictionary(
                           x => x.Split(':').First(),
                           x => x.Split(':').Last()
                           ))
                   .ToList();

        public static bool ValidByr(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("byr", out var value);
            if (isThere)
            {
                var isInt = int.TryParse(value, out var year);
                if (isInt)
                    return year >= 1920 && year <= 2002;
                else return false;
            }
            else return false;
        }
        public static bool ValidIyr(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("iyr", out var value);
            if (isThere)
            {
                var isInt = int.TryParse(value, out var year);
                if (isInt)
                    return year >= 2010 && year <= 2020;
                else return false;
            }
            else return false;
        }
        public static bool ValidEyr(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("eyr", out var value);
            if (isThere)
            {
                var isInt = int.TryParse(value, out var year);
                if (isInt)
                    return year >= 2020 && year <= 2030;
                else return false;
            }
            else return false;
        }
        public static bool ValidHgt(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("hgt", out var value);
            if (isThere)
            {
                var regex = new Regex("(\\d+)(\\w+)");
                var match = regex.Match(value);
                if (match.Success)
                {
                    if (int.TryParse(match.Groups[1].Value, out int height))
                    {
                        var unit = match.Groups[2].Value;
                        if (unit == "cm") return height >= 150 && height <= 193;
                        else if (unit == "in") return height >= 59 && height <= 76;
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
        public static bool ValidEcl(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("ecl", out var value);
            if (isThere)
            {
                return value switch
                {
                    "amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth" => true,
                    _ => false,
                };
            }
            else return false;
        }
        public static bool ValidPid(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("pid", out var value);
            if (isThere)
            {
                var regex = new Regex("^[\\d]{9}$");
                return regex.IsMatch(value);
            }
            else return false;
        }
        public static bool ValidHcl(Dictionary<string, string> dict)
        {
            var isThere = dict.TryGetValue("hcl", out var value);
            if (isThere)
            {
                var regex = new Regex("#[0-9a-f]{6}");
                var match = regex.Match(value);
                return match.Success;
            }
            else return false;
        }
    }
}
