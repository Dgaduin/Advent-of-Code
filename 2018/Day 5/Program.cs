using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string t = File.ReadAllText("input.txt");
            //Console.WriteLine(s);
            HashSet<char> set = new HashSet<char>(t.ToUpper());
            int min = Int32.MaxValue;
            foreach (char c in set)
            {
                var s = t.Replace(c.ToString(), String.Empty).Replace(Char.ToLower(c).ToString(), String.Empty);
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (i < 0) i = 0;
                    if (s[i] == s[i + 1])
                        continue;

                    if (Char.IsLower(s[i]) && Char.IsUpper(s[i + 1]))
                    {
                        if (s[i] == Char.ToLower(s[i + 1]))
                        {
                            s = s.Remove(i, 2);
                            i -= 2;
                            // Console.WriteLine(s);
                            continue;
                        }
                    }
                    if (Char.IsUpper(s[i]) && Char.IsLower(s[i + 1]))
                    {
                        if (s[i] == Char.ToUpper(s[i + 1]))
                        {
                            s = s.Remove(i, 2);
                            i -= 2;
                            //Console.WriteLine(s);
                            continue;
                        }
                    }
                }
                if (s.Length < min) min = s.Length;
            }
            Console.WriteLine(min);
        }
    }
}
