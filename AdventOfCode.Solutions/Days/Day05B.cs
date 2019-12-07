using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day5B : Day5A
    {
        public override string Solve()
        {
            var program = _parser.GetData().Split(',').Select(int.Parse).ToList();

            var output = IntcodeComputer.Compute(program, 5);

            return output.ToString();
        }
    }
}
