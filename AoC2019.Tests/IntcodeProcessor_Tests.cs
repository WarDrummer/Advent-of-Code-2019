using AdventOfCode.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Tests
{
    public class IntcodeProcessor_Tests
    {
       // [TestCase(1002, 4, 3, 4, 33)]
        [TestCase(3, 0, 4, 0, 99)]
        public void test(params int[] program)
        {
            var p = new IntcodeProcessor(Intcodes.Day05Codes, program);
            p.Execute();
            Assert.AreEqual(3,p.Output);
        }
    }
}
