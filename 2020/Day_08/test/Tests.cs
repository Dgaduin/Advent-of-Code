using System;
using System.Collections.Generic;
using Xunit;

namespace Day_08.Test
{
    public class Test
    {
        [Fact]
        public void Task1_1()
        {
            var input = new List<string>{
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6",
            };
            Assert.Equal(5, Program.Task1(input));
        }

        [Fact]
        public void Task2_1()
        {
            var input = new List<string>{
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6",
            };
            Assert.Equal(8, Program.Task2(input));
        }
    }
}
