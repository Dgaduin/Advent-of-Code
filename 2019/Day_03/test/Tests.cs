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
            Assert.Equal("159", Program.Task1(new List<string> { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }));
        }

        [Fact]
        public void Task1_2()
        {
            Assert.Equal("135", Program.Task1(new List<string> { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }));
        }

        [Fact]
        public void Task2_1()
        {
            Assert.Equal("610", Program.Task2(new List<string> { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }));
        }

        [Fact]
        public void Task2_2()
        {
            Assert.Equal("410", Program.Task2(new List<string> { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }));
        }
    }
}
