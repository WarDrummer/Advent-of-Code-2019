using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Days
{
    public partial class Day13A
    {
        public class ArcadeGame
        {
            private const long WALL = 1;
            private const long BLOCK = 2;
            private const long PADDLE = 3;
            private const long BALL = 4;

            private Dictionary<Point, long> Screen = new Dictionary<Point, long>();
            private readonly IntcodeComputer computer;
            private Queue<long> computerOutput = new Queue<long>();

            public long Score { get; private set; } = 0;

            private Point? ball = null;
            private Point? paddle = null;

            public int BlockTileCount => Screen.Values.Where(tile => tile == 2).Count();

            public ArcadeGame(List<long> program, bool render = false)
            {
                computer = new IntcodeComputer(program, 0);
                computer.OnOuput += OnComputerOutput;
                if (render)
                {
                    computer.OnInputRead += OnComputerInput;
                }
            }

            private int OnComputerInput()
            {
                // Uncomment to watch
                //Console.Clear();
                //Console.Write(PrintScreen());
                //Thread.Sleep(15);

                var dx = 0;
                if (paddle.HasValue && ball.HasValue)
                {
                    var paddleX = paddle.Value.X;
                    var ballX = ball.Value.X;
                    dx = ballX - paddleX;
                }

                return Math.Max(Math.Min(dx, 1), -1);
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
                if (computerOutput.Count == 3)
                {
                    var x = computerOutput.Dequeue();
                    var y = computerOutput.Dequeue();
                    if (x == -1 && y == 0)
                    {
                        Score = computerOutput.Dequeue();
                    }
                    else
                    {
                        var tileLocation = new Point((int)x, (int)y);
                        var tileType = computerOutput.Dequeue();

                        Screen[tileLocation] = tileType;

                        if (tileType == BALL)
                            ball = tileLocation;
                        else if (tileType == PADDLE)
                            paddle = tileLocation;
                    }
                }
            }

            public string PrintScreen()
            {
                int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
                foreach (var pt in Screen.Keys)
                {
                    if (pt.X > maxX) maxX = pt.X;
                    if (pt.X < minX) minX = pt.X;
                    if (pt.Y > maxY) maxY = pt.Y;
                    if (pt.Y < minY) minY = pt.Y;
                }

                var sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine($"Score: {Score}");
                for (var y = minY; y <= maxY; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        var pt = new Point(x, y);
                        var block = Screen.ContainsKey(pt) ? Screen[pt] : 0;
                        var symbol = ' ';
                        switch (block)
                        {
                            case WALL: symbol = '▒'; break;
                            case BLOCK: symbol = '#'; break;
                            case PADDLE: symbol = '-'; break;
                            case BALL: symbol = '*'; break;
                        }
                        sb.Append(symbol);
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
        }
    }
}
