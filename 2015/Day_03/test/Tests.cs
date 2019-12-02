using System;
using System.Collections.Generic;
using Xunit;

namespace Day_03.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            Assert.Equal("2", Program.Task1(">"));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal("4", Program.Task1("^>v<"));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.Equal("2", Program.Task1("^v^v^v^v^v"));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal("3", Program.Task2("^v"));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.Equal("3", Program.Task2("^>v<"));
        }

        [Fact]
        public void Task2_3()
        {
            Assert.Equal("11", Program.Task2("^v^v^v^v^v"));
        }
    }
}
