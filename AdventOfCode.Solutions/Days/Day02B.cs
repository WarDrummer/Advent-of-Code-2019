using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day2B : Day2A
    {
        public override string Solve()
        {
           
            var data = _parser.GetData().Split(',').Select(int.Parse).ToArray();
            var program = new List<int>();
           
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    program = data.ToList();
                    program[1] = noun;
                    program[2] = verb;

                    var output = IntcodeComputer.Compute(program);

                    if (program[0] == 19690720L)
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }

            return "Not found";
        }
    }
}
