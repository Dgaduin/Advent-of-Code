using System;
using System.Collections.Generic;
using Xunit;

namespace Day_02.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var input = new List<(string, int)>
            {
                new("forward", 5),
                new("down", 5),
                new("forward", 8),
                new("up", 3),
                new("down", 8),
                new("forward", 2)
            };

            Assert.Equal(150, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<(string, int)>
            {
                new("forward", 5),
                new("down", 5),
                new("forward", 8),
                new("up", 3),
                new("down", 8),
                new("forward", 2)
            };
            Assert.Equal(900, Program.Task2(input));
        }
    }
}
