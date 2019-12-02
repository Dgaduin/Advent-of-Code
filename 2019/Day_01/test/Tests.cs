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
            Assert.Equal(2, Program.Task1(12));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal(2, Program.Task1(14));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.Equal(654, Program.Task1(1969));
        }

        [Fact]
        public void Task1_4()
        {
            Assert.Equal(33583, Program.Task1(100756));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal(2, Program.Task2(14));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.Equal(966, Program.Task2(1969));
        }

        [Fact]
        public void Task2_3()
        {
            Assert.Equal(50346, Program.Task2(100756));
        }
    }
}
