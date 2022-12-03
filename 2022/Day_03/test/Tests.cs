using System;
using System.Collections.Generic;
using Xunit;

namespace Day_03.Test;

public class Test
{
    public IEnumerable<(HashSet<char>, HashSet<char>, char)> backpacks;
    public Test()
    {
        var input = new List<string>(){
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw"
        };
        backpacks = input.ProcessBackpacks();
    }
    [Fact]
    public void Task1_1()
    {
        Assert.Equal(157, Program.Task1(backpacks));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal("", Program.Task2());
    }
}
