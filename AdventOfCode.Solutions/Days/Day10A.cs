using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day10A : IProblem
    {
        protected readonly ParserType Parser;

        public Day10A(ParserType parser) { Parser = parser; }

        public Day10A() : this(new ParserType("day10.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData();

            return "Unsolved";
        }
    }
}
