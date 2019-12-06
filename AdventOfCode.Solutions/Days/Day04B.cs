using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day4B : Day4A
    {
        public override bool IsValid(int n)
        {
            var s = n.ToString();
            var counts = new char[256];

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] > s[i + 1])
                    return false;

                counts[s[i]]++;
            }

            counts[s[s.Length-1]]++;

            return counts.Any(c => c == 2);
        }
    }
}