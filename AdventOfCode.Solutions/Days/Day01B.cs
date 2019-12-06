using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day1B : Day1A
    {
        //protected readonly ParserType _parser;

        //public Day1B() : this(new ParserType("day01.in")) { }

        //private Day1B(ParserType parser) { _parser = parser; }

        public override string Solve()
        {
            var input = _parser.GetData().Select(int.Parse).Select(CalculateCompoundFuelNeeded);
            return input.Sum().ToString();
        }

        private static int CalculateCompoundFuelNeeded(int num)
        {
            var total = 0;
            var fuel = num;
            while ((fuel = CalculateFuelNeeded(fuel)) > 0)
            {
                total += fuel;
            }

            return total;
        }

       
    }
}

