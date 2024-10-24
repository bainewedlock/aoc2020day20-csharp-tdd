using System.Runtime.InteropServices;

namespace aoc2020day20_csharp
{
    public class PuzzleTeil
    {
        public int id;
        public string[] lines;
        public string top;
        public string left;
        public string bottom;
        public string right;

        public PuzzleTeil(string input)
        {
            var lines = input.Trim().Split("\r\n");
            id = int.Parse(lines[0].Replace("Tile ", "").Replace(":", ""));

            this.lines = lines.Skip(1).Select(x => x.Trim()).ToArray();

            top = this.lines[0];
            bottom = this.lines[^1];
            left = new String(this.lines.Select(x => x[0]).ToArray());
            right = new String(this.lines.Select(x => x[^1]).ToArray());
        }

        public bool PasstZuRand(string rand)
        {
            return
                Check(left, rand) || Check(top, rand) ||
                Check(bottom, rand) || Check(right, rand);
        }

        bool Check(string rand, string rand2)
        {
            return rand == rand2 || rand == Rev(rand2);
        }

        string Rev(string left)
        {
            return new string(left.Reverse().ToArray());
        }
    }
}
