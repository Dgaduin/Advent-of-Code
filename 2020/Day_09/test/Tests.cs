using System;
using System.Collections.Generic;
using Xunit;

namespace Day_09.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var input = new List<long> { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576, };
            Assert.Equal(127, Program.Task1(input, 5));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<long> { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576, };
            Assert.Equal(62, Program.Task2(input, 127));
        }
    }
}
