using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day16A : IProblem
    {
		protected readonly ParserType Parser;

        public Day16A(ParserType parser) { Parser = parser; }

        public Day16A() : this(new ParserType("day16.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData();

            return "Unsolved";
        }

    }
}
