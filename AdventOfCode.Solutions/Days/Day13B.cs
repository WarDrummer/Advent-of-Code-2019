using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day13B : Day13A
    {
        public override string Solve()
        {
            var program = Parser.GetData().Split(',').Select(long.Parse).ToList();

            program[0] = 2;
            var arcade = new ArcadeGame(program, true);
            arcade.Go();

            return arcade.Score.ToString();
        }
    }
}
