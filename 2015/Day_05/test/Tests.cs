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
            Assert.True(Program.IsNice1("ugknbfddgicrmopn"));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.False(Program.IsNice1("haegwjzuvuyypxyu"));
        }

        [Fact]
        public void Task1_3()
        {
            Assert.True(Program.IsNice1("aaa"));
        }

        [Fact]
        public void Task1_4()
        {
            Assert.False(Program.IsNice1("jchzalrnumimnmhp"));
        }

        [Fact]
        public void Task1_5()
        {
            Assert.False(Program.IsNice1("dvszwmarrgswjxmb"));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.True(Program.IsNice2("qjhvhtzxzqqjkmpb"));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.True(Program.IsNice2("xxyxx"));
        }

        [Fact]
        public void Task2_3()
        {
            Assert.False(Program.IsNice2("uurcxstgmygtbstg"));
        }

        [Fact]
        public void Task2_4()
        {
            Assert.False(Program.IsNice2("ieodomkazucvgmuy"));
        }
    }
}
