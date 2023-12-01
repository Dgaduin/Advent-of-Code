namespace Day_01;
public static class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadLines("input.txt").ToList();
        // var input = File.ReadAllText("input.txt");

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

        foreach (var digit in DigitsChars)
        {
            var index = line.IndexOf(digit);
            if (index != -1 && index < first.Index)
            {
                first.Index = index;
                first.Value = digit - '0';
            }
            var indexLast = line.LastIndexOf(digit);
            if (indexLast != -1 && indexLast > last.Index)
            {
                last.Index = indexLast;
                last.Value = digit - '0';
            }
        }

        foreach (var digit in DigitsStrings)
        {
            var index = line.IndexOf(digit);
            if (index != -1 && index < first.Index)
            {
                first.Index = index;
                first.Value = DigitWords[digit];
            }
            var indexLast = line.LastIndexOf(digit);
            if (indexLast != -1 && indexLast > last.Index)
            {
                last.Index = indexLast;
                last.Value = DigitWords[digit];
            }
        }
        Console.WriteLine($"{line} {first.Value * 10 + last.Value}");
        return first.Value * 10 + last.Value;
    }
    static readonly char[] DigitsChars = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
    static readonly string[] DigitsStrings = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
    static readonly Dictionary<string, int> DigitWords = new(){
        { "one", 1},
        { "two", 2},
        { "three", 3},
        { "four", 4},
        { "five", 5},
        { "six", 6},
        { "seven", 7},
        { "eight", 8},
        { "nine", 9},
    };
}
