using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_09.Test;

public class Test
{
    List<string> input;
    public Test()
    {
        input = new List<string>
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(13, Program.Task1(input));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal("", Program.Task2());
    }
}
