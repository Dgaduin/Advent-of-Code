using System;
using System.Collections.Generic;
using Xunit;

namespace Day_01.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var input = new List<int> { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263, };
            Assert.Equal(7, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<int> { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263, };
            Assert.Equal(5, Program.Task2(input));
        }
    }
}
