using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Day_03.Test;

public class Test
{
    public List<char> task1Commons;
    public List<HashSet<char>> task2Backpacks;
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
        task1Commons = input.Task1Commons().ToList();
        task2Backpacks = input.Select(x => x.ToHashSet()).ToList();
    }
    [Fact]
    public void Task1_1()
    {
        Assert.Equal(157, Program.Task1(task1Commons));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(70, Program.Task2(task2Backpacks));
    }
}
