using System.Text;

namespace AdventOfCode.Solutions.Days
{
    public class Day8B : Day8A
    {
        const char BLACK = '0';
        const char WHITE = '1';
        const char TRANSPARENT = '2';
        public override string Solve()
        {
            var input = Parser.GetData().ToCharArray();

            var numPixels = 25 * 6;
            var numLayers = input.Length / numPixels;
            var output = new int[6][];
            for(var i = 0; i < 6;i++)
            {
                output[i] = new int[25];
            }

            for(var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 25; x++)
                {
                    var i = (y*25) + x;
                    for(var z = 0; z < numLayers; z++)
                    {
                        if(input[i] == TRANSPARENT)
                        {
                            i += 150;
                            continue;
                        }
                        var color = input[i];
                        output[y][x] = color;
                        break;
                    }
                }
            }

            var result = new StringBuilder();
            result.AppendLine();
            for (var y = 0; y < 6; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x < 25; x++)
                    sb.Append(output[y][x] == BLACK ? ' ' : '▒');

                result.AppendLine(sb.ToString());
            }

            return result.ToString();
        }
    }
}
