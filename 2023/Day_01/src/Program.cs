namespace Day_01;
public static class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").ToList();
        Console.WriteLine(Task1(input));
        Console.WriteLine(Task2(input));
    }

    public static int Task1(List<string> input) => input.Sum(GetDigits);
    public static int GetDigits(string line) => (line.First(Char.IsDigit) - '0') * 10 + (line.Last(Char.IsDigit) - '0');

    public static int Task2(List<string> input) => input.Sum(GetDigits2);
    public static int GetDigits2(string line)
    {
        (int Index, int Value) first = (99, 0);
        (int Index, int Value) last = (-2, 0);

        foreach (var digit in DigitWords.Keys)
        {
            var index = line.IndexOf(digit);
            if (index != -1 && index < first.Index)
                first = (index, DigitWords[digit]);
            var indexLast = line.LastIndexOf(digit);
            if (indexLast != -1 && indexLast > last.Index)
                last = (indexLast, DigitWords[digit]);
        }
        return first.Value * 10 + last.Value;
    }

    static readonly Dictionary<string, int> DigitWords = new(){
        { "1", 1},        { "one",   1},
        { "2", 2},        { "two",   2},
        { "3", 3},        { "three", 3},
        { "4", 4},        { "four",  4},
        { "5", 5},        { "five",  5},
        { "6", 6},        { "six",   6},
        { "7", 7},        { "seven", 7},
        { "8", 8},        { "eight", 8},
        { "9", 9},        { "nine",  9},
    };
}
