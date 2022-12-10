using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_08.Test;

public class Test
{
    List<string> input;

    public Test() =>
        input = new List<string>
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(21, Program.Task1(input));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(8, Program.Task2(input));
    }
}
