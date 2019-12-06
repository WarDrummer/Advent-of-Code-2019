using AdventOfCode.Solutions.Parsers;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day16B : Day16A
    {
        protected new readonly ParserType Parser;

        public Day16B(ParserType parser) { Parser = parser; }

        public Day16B() : this(new ParserType("day16.in")) { }
    
        public override string Solve()
        {
            var input = Parser.GetData();

            return "Unsolved";
        }

    }
}
