using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day4A : IProblem
    {
        protected readonly ParserType _parser;

        public Day4A(ParserType parser) { _parser = parser; }

        public Day4A() : this(new ParserType("day04.in")) { }

        public virtual string Solve()
        {
            var range = _parser.GetData().Split('-').Select(int.Parse).ToArray();
            var count = 0;
            for(var i = range[0]; i < range[1]; i++)
            {
                if (IsValid(i))
                    count++;
            }

            return count.ToString();
        }

        public virtual bool IsValid(int n)
        {
            var s = n.ToString();
            var counts = new char[256];

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] > s[i + 1])
                    return false;

                counts[s[i]]++;
            }

            counts[s[s.Length - 1]]++;

            return counts.Any(c => c > 1);
        }
    }
}
