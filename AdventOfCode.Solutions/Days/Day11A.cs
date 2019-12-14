using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day11A : IProblem
    {
        protected readonly ParserType Parser;

        public Day11A(ParserType parser) { Parser = parser; }
        
        public Day11A() : this(new ParserType("day11.in")) { }

        public virtual string Solve()
        {
            var program = Parser.GetData().Split(',').Select(long.Parse).ToList();

            var robot = new PaintingRobot(program);
            robot.Go();

            return robot.PaintedCells.ToString();
        }
    }
}
