using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day8A : IProblem
    {
        protected readonly ParserType Parser;

        public Day8A(ParserType parser) { Parser = parser; }

        public Day8A() : this(new ParserType("day08.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData();

            return "Unsolved";
        }

    }

}
