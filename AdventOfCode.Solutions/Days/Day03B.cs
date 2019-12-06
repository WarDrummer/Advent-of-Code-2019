using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day3B : Day3A
    {
        public override string Solve()
        {
            var input = _parser.GetData().Select(s => s.Split(',').ToList()).ToArray();
            var wire1 = input[0];
            var wire2 = input[1];

            var pts1 = GetPoints(wire1).ToList();
            var pts2 = GetPoints(wire2).ToList();

            var pts1Lookup = pts1.ToHashSet();
            var intersections = new HashSet<Point>();
            foreach (var pt in pts2)
            {
                if (pts1Lookup.Contains(pt) && !intersections.Contains(pt))
                {
                    intersections.Add(pt);
                }
            }

            var min = int.MaxValue;
            foreach(var intersection in intersections)
            {
                var steps1 = pts1.IndexOf(intersection) + 1;
                var steps2 = pts2.IndexOf(intersection) + 1;
                var total = steps1 + steps2;
                if (total < min)
                    min = total;
            }

            return min.ToString();
        }
    }
}
