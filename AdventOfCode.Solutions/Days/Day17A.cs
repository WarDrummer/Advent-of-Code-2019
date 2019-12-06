using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day17A : IProblem
    {
        protected readonly ParserType _parser;

        public Day17A(ParserType parser) { _parser = parser; }

        public Day17A() : this(new ParserType("day17.in")) { }

        public virtual string Solve()
        {
            var input = _parser.GetData();

            return "Unsolved";
        }
    }
}
