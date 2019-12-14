using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public partial class Day13A : IProblem
    {
        protected readonly ParserType Parser;

        public Day13A(ParserType parser) { Parser = parser; }

        public Day13A() : this(new ParserType("day13.in")) { }

        public virtual string Solve()
        {
            var program = Parser.GetData().Split(',').Select(long.Parse).ToList();

            var arcade = new ArcadeGame(program);
            arcade.Go();

            return arcade.BlockTileCount.ToString();
        }
    }
}
