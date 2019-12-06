using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day5A : IProblem
    {
        protected readonly ParserType _parser;

        public Day5A(ParserType parser) { _parser = parser; }

        public Day5A() : this(new ParserType("day05.in")) { }

        public virtual string Solve()
        {
            var program = _parser.GetData().Split(',').Select(int.Parse).ToList();

            var output = IntcodeComputer.Compute(program, 1);

            return output.ToString();
        }
    }
}
