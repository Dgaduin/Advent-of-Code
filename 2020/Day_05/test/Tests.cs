using System;
using System.Collections.Generic;
using Xunit;

namespace Day_05.Test
{
    public class Test
    {
        [Fact]
        public void BinaryPartition_1()
        {
            var t = Program.BinaryPartition("FBFBBFF", 'F', 0, 127);
            Assert.Equal(44, t);
        }
        [Fact]
        public void BinaryPartition_2()
        {
            var t = Program.BinaryPartition("RLR", 'L', 0, 7);
            Assert.Equal(5, t);
        }
        [Fact]
        public void GetId_1() => Assert.Equal(567, Program.GetId("BFFFBBFRRR"));
        [Fact]
        public void GetId_2() => Assert.Equal(119, Program.GetId("FFFBBBFRRR"));
        [Fact]
        public void GetId_3() => Assert.Equal(820, Program.GetId("BBFFBBFRLL"));
        [Fact]
        public void GetId_4() => Assert.Equal(357, Program.GetId("FBFBBFFRLR"));
    }
}
