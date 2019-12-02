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
            Assert.Equal("609043", Program.Task1("abcdef"));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal("1048970", Program.Task1("pqrstuv"));
        }
    }
}
