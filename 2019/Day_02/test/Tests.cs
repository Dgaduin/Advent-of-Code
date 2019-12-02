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
            Assert.Equal(new int[] { 2, 0, 0, 0, 99 }, Program.Task1(new int[] { 1, 0, 0, 0, 99 }));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal(new int[] { 2, 3, 0, 6, 99 }, Program.Task1(new int[] { 2, 3, 0, 3, 99 }));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.Equal(new int[] { 2, 4, 4, 5, 99, 9801 }, Program.Task1(new int[] { 2, 4, 4, 5, 99, 0 }));
        }

        [Fact]
        public void Task1_4()
        {
            Assert.Equal(new int[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 }, Program.Task1(new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }));
        }
    }
}
