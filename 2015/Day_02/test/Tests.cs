using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Day_02.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            Assert.Equal("58", Program.Task1(new List<string> { "2x3x4" }));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal("43", Program.Task1(new List<string> { "1x1x10" }));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal("34", Program.Task2(new List<string> { "2x3x4" }));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.Equal("14", Program.Task2(new List<string> { "1x1x10" }));
        }
    }
}
