using System;
using System.Collections.Generic;
using Xunit;

namespace Day_04.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            Assert.True(Program.IsPasswordValid1("111111"));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.False(Program.IsPasswordValid1("223450"));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.False(Program.IsPasswordValid1("123789"));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.True(Program.IsPasswordValid2("112233"));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.False(Program.IsPasswordValid2("123444"));
        }

        [Fact]
        public void Task2_3()
        {
            Assert.True(Program.IsPasswordValid2("111122"));
        }
    }
}
