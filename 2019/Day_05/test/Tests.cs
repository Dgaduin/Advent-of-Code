using System;
using System.Collections.Generic;
using Xunit;

namespace Day_05.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            Assert.Equal(new int[] { 1002, 4, 3, 4, 99 }, Program.Task1(new int[] { 1002, 4, 3, 4, 33 }));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal("", Program.Task2());
        }
    }
}
