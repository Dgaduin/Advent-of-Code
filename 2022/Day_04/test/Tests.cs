using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_04.Test;

public class Test
{
    public List<List<int>> pairs;
    public Test()
    {
        var testData = new List<string>()
        {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8"
        };
        pairs = testData.ReadPairs();
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(2, Program.Task1(pairs));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal("", Program.Task2());
    }
}
