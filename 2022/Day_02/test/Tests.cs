using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_02.Test;

public class Test
{
    public List<(int, int)> payload;

    public Test()
    {
        payload = new List<(int, int)>
        {
            (1, 2),
            (2, 1),
            (3, 3),
        };
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(15, Program.Task1(payload));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal("", Program.Task2());
    }
}
