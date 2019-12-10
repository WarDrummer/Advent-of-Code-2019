using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day9B : Day9A
    {
        public override string Solve()
        {
            var data = _parser.GetData().Split(',').Select(long.Parse).ToArray();

            var input = 2;
            var program = data.ToList();
            var amp = new Amplifier(program, input);
            amp.Compute();

            return amp.Output.ToString();
        }
    }
}
