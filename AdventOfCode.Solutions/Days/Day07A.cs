using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day7A : IProblem
    {
        protected readonly ParserType _parser;

        public Day7A(ParserType parser) { _parser = parser; }

        public Day7A() : this(new ParserType("day07.in")) { }

        public virtual string Solve()
        {
            var input = _parser.GetData();

            return "Unsolved";
        }
    }

}
