using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day7A : IProblem
    {
        protected readonly ParserType _parser;

        public Day7A(ParserType parser) { _parser = parser; }

        public Day7A() : this(new ParserType("day07.in")) { }

        public virtual string Solve()
        {
            var data = _parser.GetData().Split(',').Select(int.Parse).ToArray();
            List<int> program;

            var max = int.MinValue;            
            foreach (var phaseSetting in EnumeratePhaseSettings())
            {
                var input = 0;
                var output = 0;

                for (var i = 0; i < 5; i++)
                {
                    program = data.ToList();
                    output = IntcodeComputer.Compute(program, input, phaseSetting[i]);
                    input = output;
                }

                if (output > max)
                {
                    max = output;
                }
            }

            return max.ToString();
        }

        public static string PhaseSettingsToString(int[] p)
        {
            return $"{p[0]}, {p[1]}, {p[2]}, {p[3]}, {p[4]}";
        }

        public IEnumerable<int[]> EnumeratePhaseSettings(int min = 0, int max = 4)
        {
            //yield return new int[] { 1, 0, 4, 3, 2 };
            
            for (var i = min; i <= max; i++)
                for (var j = min; j <= max; j++)
                    for (var k = min; k <= max; k++)
                        for (var l = min; l <= max; l++)
                            for (var m = min; m <= max; m++)
                            {
                                var phaseSettings = new int[] { i, j, k, l, m };
                                if(phaseSettings.Distinct().Count() == 5)
                                    yield return phaseSettings;
                            }
        }
    }

}
