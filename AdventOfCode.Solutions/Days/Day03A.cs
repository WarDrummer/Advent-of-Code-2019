using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using System;
    using System.Collections.Generic;
    using ParserType = MultiLineStringParser;

    public class Day3A : IProblem
    {
        protected readonly ParserType _parser;

        public Day3A(ParserType parser) { _parser = parser; }

        public Day3A() : this(new ParserType("day03.in")) { }
        
        public virtual string Solve()
        {
            var input = _parser.GetData().Select(s => s.Split(',').ToList()).ToArray();
            var wire1 = input[0];
            var wire2 = input[1];

            var pts1 = GetPoints(wire1).ToHashSet();
            var intersections = new HashSet<Point>();
            foreach(var pt in GetPoints(wire2))
            {
                if (pts1.Contains(pt) && !intersections.Contains(pt))
                {
                    intersections.Add(pt);
                }
            }

            var manhattens = intersections.Select(i => Math.Abs(i.X) + Math.Abs(i.Y)).ToList();
            return manhattens.Min().ToString();
        }

        public IEnumerable<Point> GetPoints(IList<string> directions)
        {
            var start = new Point(0, 0);
            foreach (var direction in directions)
            {
                var distance = int.Parse(direction.Substring(1));
                switch (direction[0])
                {
                    case 'U':
                        {
                            for (var i = 1; i <= distance; i++)
                                yield return new Point(start.X, start.Y + i);
                            start = new Point(start.X, start.Y + distance);
                        }
                        break;
                    case 'D':
                        {
                            for (var i = 1; i <= distance; i++)
                                yield return new Point(start.X, start.Y - i);
                            start = new Point(start.X, start.Y - distance);
                        }
                        break;
                    case 'L':
                        {
                            for (var i = 1; i <= distance; i++)
                                yield return new Point(start.X - i, start.Y);
                            start = new Point(start.X - distance, start.Y);
                        }
                        break;
                    case 'R':
                        {
                            for (var i = 1; i <= distance; i++)
                                yield return new Point(start.X + i, start.Y);
                            start = new Point(start.X + distance, start.Y);
                        }
                        break;
                    default: throw new Exception("Unknown direction");
                }
            }
        }
    }
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEquivalent(Point p)
        {
            return X == p.X && Y == p.Y;
        }

        public int GetDistanceTo(Point p)
        {
            return Math.Abs(p.X - X) + Math.Abs(p.Y - Y);
        }

        public override int GetHashCode()
        {
            return $"{X},{Y}".GetHashCode();
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
