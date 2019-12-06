using System;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day1A : IProblem
    {
        protected readonly ParserType _parser;

        public Day1A() : this(new ParserType("day01.in")) { }

        private Day1A(ParserType parser) { _parser = parser; }

        public virtual string Solve()
        {
            var input = _parser.GetData().Select(int.Parse).Select(CalculateFuelNeeded);
            return input.Sum().ToString();
        }

        protected static int CalculateFuelNeeded(int num)
        {
            var fuel = (int)Math.Floor(num / 3d) - 2;
            return fuel > 0 ? fuel : 0;
        }
    }
}
