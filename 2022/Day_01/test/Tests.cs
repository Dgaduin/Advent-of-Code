using System;
using System.Collections.Generic;
using Xunit;

namespace Day_01.Test
{
    public class Test
    {
        public List<List<int>> Elfs;
        public Test()
        {
            var input = new List<string>{
                "1000",
                "2000",
                "3000",
                "",
                "4000",
                "",
                "5000",
                "6000",
                "",
                "7000",
                "8000",
                "9000",
                "",
                "10000"
            };
        }
        [Fact]
        public void Task1_1()
        {
            Assert.Equal(24000, Program.Task1(Elfs));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal("", Program.Task2());
        }
    }
}
