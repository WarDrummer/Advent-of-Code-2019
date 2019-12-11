using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day10B : Day10A
    {
        public override string Solve()
        {
            var laser = new Point(20, 19); //best point from day10A

            var input = Parser.GetData();
            var asteroids = GetAsteroidsFromInput(input, laser);
            asteroids = asteroids.OrderBy(a => a.Angle).ThenBy(a => a.Distance).ToList();
            var index = asteroids.FindIndex(a => a.Angle == -90);

            var destroyed = 0;
            var previousBeam = 0d;
            Asteroid previousDestroyed = null;
            while (destroyed < 200)
            {
                var asteroid = asteroids[index];
                if(asteroid.IsInBeam(previousBeam))
                {
                    index = (index + 1) % asteroids.Count;
                    continue;
                }

                previousBeam = asteroid.Angle;
                previousDestroyed = asteroids[index];
                asteroids.RemoveAt(index);
                destroyed++;
                index = index % asteroids.Count;
            }

            return previousDestroyed != null ?
                (previousDestroyed.Point.X * 100 + previousDestroyed.Point.Y).ToString() : "Unsolved";   
        }

        protected class Asteroid
        {
            public Point Point { get; }
            public double Distance { get; }
            public double Angle { get; }

            public Asteroid(Point point, Point laser)
            {
                Point = point; // storing original location for calculating result

                // offsetting points to put laser at 0,0
                var offsetPoint = new Point(point.X - laser.X, point.Y - laser.Y);

                // use polar coordinate components for distance and angle 
                Distance = Math.Sqrt(offsetPoint.X * offsetPoint.X + offsetPoint.Y * offsetPoint.Y);
                Angle = Math.Atan2(offsetPoint.Y, offsetPoint.X) * 180 / Math.PI;
            }

            public override string ToString()
            {
                return $"{Point}: {Angle }, {Distance} ";
            }

            public bool IsInBeam(double beamAngle)
            {
                return Math.Abs(beamAngle - Angle) < 0.01;
            }
        }

        protected static List<Asteroid> GetAsteroidsFromInput(IEnumerable<string> input, Point laser)
        {
            var asteroids = new List<Asteroid>();
            var y = 0;
            foreach (var row in input)
            {
                var x = 0;
                foreach (var c in row)
                {
                    var pt = new Point(x, y);
                    if (c == '#' && !pt.IsEquivalent(laser))
                        asteroids.Add(new Asteroid(pt, laser));
                    x++;
                }
                y++;
            }

            return asteroids;
        }
    }
}
