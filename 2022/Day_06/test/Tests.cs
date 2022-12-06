using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_06.Test;

public class Test
{
    public Test() { }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(7, Program.Task1("mjqjpqmgbljsphdztnvjfqwrcgsmlb"));
    }
    [Fact]
    public void Task1_2()
    {
        Assert.Equal(5, Program.Task1("bvwbjplbgvbhsrlpgdmjqwftvncz"));
    }
    [Fact]
    public void Task1_3()
    {
        Assert.Equal(6, Program.Task1("nppdvjthqldpwncqszvftbrmjlhg"));
    }
    [Fact]
    public void Task1_4()
    {
        Assert.Equal(10, Program.Task1("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"));
    }
    [Fact]
    public void Task1_5()
    {
        Assert.Equal(11, Program.Task1("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(19, Program.Task2("mjqjpqmgbljsphdztnvjfqwrcgsmlb"));
    }
    [Fact]
    public void Task2_2()
    {
        Assert.Equal(23, Program.Task2("bvwbjplbgvbhsrlpgdmjqwftvncz"));
    }
    [Fact]
    public void Task2_3()
    {
        Assert.Equal(23, Program.Task2("nppdvjthqldpwncqszvftbrmjlhg"));
    }
    [Fact]
    public void Task2_4()
    {
        Assert.Equal(29, Program.Task2("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"));
    }
    [Fact]
    public void Task2_5()
    {
        Assert.Equal(26, Program.Task2("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"));
    }
}
