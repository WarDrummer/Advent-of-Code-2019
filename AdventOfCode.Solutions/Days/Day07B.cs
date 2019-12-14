using AdventOfCode.Solutions.Extensions;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day7B : Day7A
    {
        public override string Solve()
        {
            var data = _parser.GetData().Split(',').Select(long.Parse).ToArray();
            long max = int.MinValue;

            foreach (var phaseSetting in EnumeratePhaseSettings(5,9))
            {
                IntcodeComputer[] amplifiers = {
                    new IntcodeComputer(data.ToList(), phaseSetting[0]),
                    new IntcodeComputer(data.ToList(), phaseSetting[1]),
                    new IntcodeComputer(data.ToList(), phaseSetting[2]),
                    new IntcodeComputer(data.ToList(), phaseSetting[3]),
                    new IntcodeComputer(data.ToList(), phaseSetting[4]),
                };

                long input = 0;
                while (!amplifiers[4].IsHalted)
                {
                    foreach (var amp in amplifiers)
                    {
                        amp.SetInput(input);
                        amp.Compute();
                        input = amp.Output;
                    }
                }

                if (amplifiers[4].Output > max)
                {
                    max = amplifiers[4].Output;
                }
            }

            return max.ToString();
        }
    }
}
