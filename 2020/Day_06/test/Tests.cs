using System;
using System.Collections.Generic;
using Xunit;

namespace Day_06.Test
{
    public class Test
    {
        List<string> input = new List<string> { "abc", "", "a", "b", "c", "", "ab", "ac", "", "a", "a", "a", "a", "", "b" };

        [Fact]
        public void Task1_1()
        {
            Assert.Equal(11, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal(6, Program.Task2(input));
        }
    }
}
