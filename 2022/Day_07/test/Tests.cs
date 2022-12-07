using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Day_07;

namespace Day_07.Test;

public class Test
{
    public Directory root;
    public Test()
    {
        var input = new List<string>()
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k"
        };
        root = input.ParseInput();
    }

    [Fact]
    public void Task1_1()
    {
        Assert.Equal(95437, Program.Task1(root));
    }

    [Fact]
    public void Task2_1()
    {
        Assert.Equal(24933642, Program.Task2(root));
    }
}
