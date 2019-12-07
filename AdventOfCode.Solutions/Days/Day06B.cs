using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;
    public class Day6B : IProblem
    {
        protected readonly ParserType _parser;

        public Day6B(ParserType fileParser) { _parser = fileParser; }

        public Day6B() : this(new ParserType("day06.in")) { }

        public string Solve()
        {
            var orbits = new Dictionary<string, List<string>>();
            var orbitsInputs = _parser.GetData();
            foreach (var orbit in orbitsInputs)
            {
                
                var parts = orbit.Split(')');
                var o1 = parts[0];
                var o2 = parts[1];
                if (!orbits.ContainsKey(o1))
                    orbits[o1] = new List<string>();
                if (!orbits.ContainsKey(o2))
                    orbits[o2] = new List<string>();

                if (!orbits[o1].Contains(o2))
                    orbits[o1].Add(o2);
                if (!orbits[o2].Contains(o1))
                    orbits[o2].Add(o1);
            }

            var found = false;
            var seen = new HashSet<string>();
            var q = new Queue<string>();
            var nextQ = new Queue<string>();
            q.Enqueue("YOU");
            seen.Add("YOU");
            var distance = -1; // don't include YOU
            var targets = new HashSet<string>(orbits["SAN"]);
            while(q.Count > 0)
            {
                var current = q.Dequeue();
                if (targets.Contains(current))
                {
                    found = true;
                    break;
                }

                if (orbits.ContainsKey(current))
                {
                    foreach(var o in orbits[current])
                    {
                        if (!seen.Contains(o))
                        {
                            nextQ.Enqueue(o);
                            seen.Add(o);
                        }
                    }
                }

                if (q.Count == 0)
                {
                    q = nextQ;
                    nextQ = new Queue<string>();
                    distance++;
                }
            }

            return found ? (distance).ToString() : "Not Found";
        }
    }
}
