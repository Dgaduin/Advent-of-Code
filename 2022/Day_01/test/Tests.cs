using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_01.Test;
public class Test
{
    public List<List<int>> Elfs;
    public Test()
    {
        var input = new List<string>{
                "1000",
                "2000",
                "3000",
                "",
                "4000",
                "",
                "5000",
                "6000",
                "",
                "7000",
                "8000",
                "9000",
                "",
                "10000"
            };
        Elfs = Program.ChunkIntoElfs(input).ToList();
    }
    [Fact]
    public void Task1_1()
    {
        Assert.Equal(24000, Program.Task1(Elfs));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(45000, Program.Task2(Elfs));
    }
}
