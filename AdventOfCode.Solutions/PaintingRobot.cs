using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Solutions.Days
{
    public class PaintingRobot
    {
        private enum Direction { UP, DOWN, LEFT, RIGHT };

        private int direction = 0;

        private static Direction[] compass
            = new Direction[] { Direction.UP, Direction.RIGHT, Direction.DOWN, Direction.LEFT };

        private Point location = new Point();
        private Dictionary<Point, long> hull = new Dictionary<Point, long>();
        private readonly IntcodeComputer computer;
        private Queue<long> computerOutput = new Queue<long>();

        public int PaintedCells => hull.Keys.Count;

        public PaintingRobot(List<long> program, long startingCellColor = 0)
        {
            computer = new IntcodeComputer(program, startingCellColor);
            computer.OnOuput += OnComputerOutput;
        }

        public void Go()
        {
            while (!computer.IsHalted)
            {
                computer.Compute();
            }
        }

        private void OnComputerOutput(long output)
        {
            computerOutput.Enqueue(output);
            if(computerOutput.Count == 2)
            {
                Paint(computerOutput.Dequeue());
                ChangeDirection(computerOutput.Dequeue());
                Move();

                computer.SetInput(GetCurrentColor());
            }
        }

        private long GetCurrentColor()
        {
            return hull.ContainsKey(location) ? hull[location] : 0;
        }

        private void Paint(long color)
        {
            hull[location] = color;
        }

        private void ChangeDirection(long change)
        {
            var prev = direction;
            if (change == 0) // left
            {
                direction = (direction + 3) % 4;
            }
            else if (change == 1)
            {
                direction = (direction + 1) % 4;
            }
            else
            {
                Debug.Assert(false, $"{change} is not a valid direction instruction; must be 0 or 1.");
            }
        }

        private void Move()
        {
            switch (compass[direction])
            {
                case Direction.UP:
                    location = new Point(location.X, location.Y + 1);
                    break;
                case Direction.DOWN:
                    location = new Point(location.X, location.Y - 1);
                    break;
                case Direction.LEFT:
                    location = new Point(location.X - 1, location.Y);
                    break;
                case Direction.RIGHT:
                    location = new Point(location.X + 1, location.Y);
                    break;
                default:
                    throw new Exception("Invalid direction; cannot Move.");
            }
        }

        public string PrintTag()
        {
            int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
            foreach(var pt in hull.Keys)
            {
                if (pt.X > maxX) maxX = pt.X;
                if (pt.X < minX) minX = pt.X;
                if (pt.Y > maxY) maxY = pt.Y;
                if (pt.Y < minY) minY = pt.Y;
            }

            var sb = new StringBuilder();
            sb.AppendLine();
            for (var y = maxY; y >= minY; y--)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    var pt = new Point(x, y);
                    var color = hull.ContainsKey(pt) ? hull[pt] : 0;
                    sb.Append((color == 1) ? '#': ' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
