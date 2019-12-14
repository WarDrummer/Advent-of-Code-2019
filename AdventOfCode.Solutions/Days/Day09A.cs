using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day9A : IProblem
    {
        protected readonly ParserType _parser;

        public Day9A(ParserType parser) { _parser = parser; }

        public Day9A() : this(new ParserType("day09.in")) { }
      
        public virtual string Solve()
        {
            var data = _parser.GetData().Split(',').Select(long.Parse).ToArray();

            var input = 1;
            var program = data.ToList();
            var amp = new IntcodeComputer(program, input);
            amp.Compute();

            return amp.Output.ToString();
        }
    }
}
