using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_02.Test;

public class Test
{
    public List<(int, int)> games;

    public Test()
    {
        games = new List<(int, int)>
        {
            (0, 1),
            (1, 0),
            (2, 2),
        };
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(15, Program.Task1(games));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(12, Program.Task2(games));
    }
}
