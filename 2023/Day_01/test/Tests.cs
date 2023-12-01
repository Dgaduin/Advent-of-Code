using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_01.Test;

public class Test
{
    public Test() { }

    [Fact]
    public void Task1_1()
    {
        var list = new List<string> { "1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet" };
        Assert.Equal(142, Program.Task1(list));
    }
    [Fact]
    public void Task2_1()
    {
        var list = new List<string> { "two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen" };
        Assert.Equal(281, Program.Task2(list));
    }

    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void TestHelper(string text, int output)
    {
        Assert.Equal(Program.GetDigits(text), output);
    }

    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("8954bxsqntndjmonenx5", 85)]
    [InlineData("4one1eightzgcpkgbpgmsevenninetwonetk", 41)]
    public void TestHelper2(string text, int output)
    {
        Assert.Equal(Program.GetDigits2(text), output);
    }
}
