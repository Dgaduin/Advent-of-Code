using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Day_01.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            Assert.Equal("0", Program.Task1("(())"));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal("0", Program.Task1("()()"));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.Equal("3", Program.Task1("(()(()("));
        }

        [Fact]
        public void Task1_4()
        {
            Assert.Equal("3", Program.Task1("((("));
        }

        [Fact]
        public void Task1_5()
        {
            Assert.Equal("3", Program.Task1("))((((("));
        }

        [Fact]
        public void Task1_6()
        {
            Assert.Equal("-1", Program.Task1("())"));
        }

        [Fact]
        public void Task1_7()
        {
            Assert.Equal("-1", Program.Task1("))("));
        }

        [Fact]
        public void Task1_8()
        {
            Assert.Equal("-3", Program.Task1(")))"));
        }

        [Fact]
        public void Task1_9()
        {
            Assert.Equal("-3", Program.Task1(")())())"));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal(Program.Task2(")"), "1");
        }

        [Fact]
        public void Task2_2()
        {
            Assert.Equal(Program.Task2("()())"), "5");
        }
    }
}
