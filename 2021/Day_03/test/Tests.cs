using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Day_03.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var inputRaw = new List<string>()
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010",
            };
            var input = inputRaw.Select(Program.ParseInput).ToList();
            Assert.Equal("198", Program.Task1(input, 5));
        }

        [Fact]
        public void Task2_1()
        {
            var inputRaw = new List<string>()
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010",
            };
            var input = inputRaw.Select(Program.ParseInput).ToList();
            Assert.Equal("230", Program.Task2(input, 5));
        }
    }
}
