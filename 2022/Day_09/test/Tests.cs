using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_09.Test;

public class Test
{
    List<string> input;
    List<string> input2;
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
        input2 = new List<string>{
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20",
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
        Assert.Equal(1, Program.Task2(input));
    }

    [Fact]
    public void Task2_2()
    {
        Assert.Equal(36, Program.Task2(input2));
    }
}
