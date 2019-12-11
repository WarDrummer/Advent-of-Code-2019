using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day10A : IProblem
    {
        protected readonly ParserType Parser;

        public Day10A(ParserType parser) { Parser = parser; }

        public Day10A() : this(new ParserType("day10.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData();
            var asteroids = GetAsteroids(input, out Point boundingPoint);
            var max = int.MinValue;
            var map = new Dictionary<Point, int>();
            var monitoringStationLocation = new Point(0, 0);
            foreach(var asteroid in asteroids)
            {
                var visible = new HashSet<PointF>();
                foreach(var otherAsteroid in asteroids)
                {
                    if (asteroid.IsEquivalent(otherAsteroid))
                        continue;

                    float slope = 0;
                    float divisor = (asteroid.X - otherAsteroid.X);
                    if (divisor != 0) 
                        slope = (asteroid.Y - otherAsteroid.Y) / divisor;
                    else // vertical line
                        slope = float.NaN;

                    float intercept = asteroid.Y - slope * asteroid.X;
                    
                    var lineOfSight = new PointF(slope, intercept, (asteroid.Y > otherAsteroid.Y)?-1:1);

                    if (!visible.Contains(lineOfSight))
                    {
                        visible.Add(lineOfSight);
                    }
                }

                map[asteroid] = visible.Count;
                if (visible.Count > max)
                {
                    max = visible.Count;
                    monitoringStationLocation = asteroid;
                }
            }
            Console.WriteLine($"Monitoring Station: {monitoringStationLocation}");
            return max.ToString();
        }

        protected static HashSet<Point> GetAsteroids(IEnumerable<string> input, out Point boundingPoint)
        {
            var asteroids = new HashSet<Point>();
            var y = 0;
            var maxX = 0;
            foreach (var row in input)
            {
                var x = 0;
                foreach (var c in row)
                {
                    var pt = new Point(x, y);
                    if (c == '#')
                        asteroids.Add(pt);
                    x++;
                }
                maxX = x;
                y++;
            }
            var maxY = y;

            boundingPoint = new Point(maxX, maxY);
            return asteroids;
        }

        public struct PointF
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public PointF(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public bool IsEquivalent(PointF p)
            {
                return GetHashCode() == p.GetHashCode();
            }

            public override int GetHashCode()
            {
                return $"{X:0.#},{Y:0.#},{Z:0.#}".GetHashCode();
            }

            public override string ToString()
            {
                return $"{X:0.#},{Y:0.#},{Z:0.#}";
            }
        }
    }
}
