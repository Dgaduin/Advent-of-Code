using System;
using System.Collections.Generic;
using Xunit;

namespace Day_12.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var moons = new List<Moon>{
                new Moon{x = -8,y =-10,z=  0},
                new Moon{x=  5, y=  5, z= 10},
                new Moon{x=  2, y= -7, z=  3},
                new Moon{x=  9, y= -8, z= -3}
            };
            Assert.Equal("1940", Program.Task1(moons, 100));
        }

        [Fact]
        public void Task2_1()
        {
            var moons = new List<Moon>{
                new Moon{x = -8,y =-10,z=  0},
                new Moon{x=  5, y=  5, z= 10},
                new Moon{x=  2, y= -7, z=  3},
                new Moon{x=  9, y= -8, z= -3}
            };
            Assert.Equal("4686774924", Program.Task2(moons));
        }
    }
}
