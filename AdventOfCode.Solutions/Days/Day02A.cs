using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day2A : IProblem
    {
        protected readonly ParserType _parser;

        public Day2A(ParserType parser) { _parser = parser; }

        public Day2A() : this(new ParserType("day02.in")) { }

        public virtual string Solve()
        {
            var noun = 12;
            var verb = 2;

            var program = _parser.GetData().Split(',').Select(int.Parse).ToList();
            program[1] = noun;
            program[2] = verb;

            var output = IntcodeComputer.Compute(program, 1);

            return program[0].ToString();
        }
    }
}
