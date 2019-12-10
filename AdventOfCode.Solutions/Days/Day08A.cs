using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day8A : IProblem
    {
        protected readonly ParserType Parser;

        public Day8A(ParserType parser) { Parser = parser; }

        public Day8A() : this(new ParserType("day08.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData().ToCharArray();

            // 25 x 6
            var numPixels = 25 * 6;
            var numLayers = input.Length / numPixels;

            var minZeroes = int.MaxValue;
            var result = 0;
            for (var i = 0; i < numLayers; i++) 
            { 
                var layerCounts = new Dictionary<char, int> { { '0', 0 }, { '1', 0 }, { '2', 0 } };
                for (var j = numPixels*i; j < numPixels*i + numPixels; j++)
                    layerCounts[input[j]]++;

                if (layerCounts['0'] < minZeroes)
                {
                    minZeroes = layerCounts['0'];
                    result = layerCounts['2'] * layerCounts['1'];
                }
            }

            return result.ToString();
        }

    }

}
