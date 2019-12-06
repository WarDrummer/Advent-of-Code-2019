using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day6A : IProblem
    {
        protected readonly ParserType _parser;

        public Day6A(ParserType fileParser) { _parser = fileParser; }

        public Day6A() : this(new ParserType("day06.in")) { }

        public virtual string Solve()
        {
            var input = _parser.GetData();

            return "Unsolved";
        }
    }

}