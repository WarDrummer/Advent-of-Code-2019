using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day6A : IProblem
    {
        protected readonly ParserType _parser;

        public Day6A(ParserType fileParser) { _parser = fileParser; }

        public Day6A() : this(new ParserType("day06.in")) { }

        public virtual string Solve()
        {
            var orbits = new Dictionary<string, string>();
            var orbitsInputs = _parser.GetData();
            var bodies = new List<string>();
            foreach(var orbit in orbitsInputs)
            {
                var parts = orbit.Split(')');
                orbits.Add(parts[1], parts[0]);
                if (!bodies.Contains(parts[0]))
                {
                    bodies.Add(parts[0]);
                }
                if (!bodies.Contains(parts[1]))
                {
                    bodies.Add(parts[1]);
                }
            }

            var orbitCount = new Dictionary<string, int>();
            orbitCount.Add("COM", 0);
            
            foreach(var body in bodies)
            {
                if (body == "COM") continue;
                var found = false;
                var count = 1;
                var current = body;
                while (!found)
                {
                    if (orbits.ContainsKey(current))
                    {
                        var next = orbits[current];
                        if (orbitCount.ContainsKey(next))
                        {
                            count += orbitCount[next];
                            found = true;
                        }
                        else
                        {
                            count++;
                        }
                        current = next;
                    } 
                    else
                    {
                        found = true;
                    }
                }
                orbitCount[body] = count;
            }

            return orbitCount.Values.Sum().ToString();
        }
    }

}