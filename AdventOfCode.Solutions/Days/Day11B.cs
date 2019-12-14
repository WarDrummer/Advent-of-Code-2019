using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day11B : Day11A
    {
        public override string Solve()
        {
            var program = Parser.GetData().Split(',').Select(long.Parse).ToList();

            var robot = new PaintingRobot(program, 1);
            robot.Go();

            return robot.PrintTag();
        }
    }
}
