using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_05.Test;

public class Test
{
    public (Stack<char>[] stacks, IEnumerable<(int, int, int)> commands) parsedInput;
    public Test()
    {
        var input = new List<string>()
        {
            "    [D]",
            "[N] [C]",
            "[Z] [M] [P]",
            " 1   2   3",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2",
        };
        parsedInput = input.ParseInput();
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal("CMZ", Program.Task1(parsedInput));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal("MCD", Program.Task2(parsedInput));
    }
}
