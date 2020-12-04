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
            var input = new List<string>{
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            Assert.Equal(2, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<string>{
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            Assert.Equal(1, Program.Task2(input));
        }
    }
}
