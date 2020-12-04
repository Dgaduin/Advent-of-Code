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
            var input = new List<int>{
                1721,
                979,
                366,
                299,
                675,
                1456
            };
            Assert.Equal(514579, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<int>{
                1721,
                979,
                366,
                299,
                675,
                1456
            };
            Assert.Equal(241861950, Program.Task2(input));
        }
    }
}
