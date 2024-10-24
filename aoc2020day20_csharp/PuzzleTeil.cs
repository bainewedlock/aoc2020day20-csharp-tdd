namespace aoc2020day20_csharp
{
    public class PuzzleTeil
    {
        public int id;
        public string[] lines;
        string[] ränder;
        public string top => ränder[0];
        public string right => ränder[1];
        public string bottom => ränder[2];
        public string left => ränder[3];

        public PuzzleTeil(string input)
        {
            var lines = input.Trim().Split("\r\n");
            id = int.Parse(lines[0].Replace("Tile ", "").Replace(":", ""));

            this.lines = lines.Skip(1).Select(x => x.Trim()).ToArray();


            var top = this.lines[0];
            var bottom = this.lines[^1];
            var left = new String(this.lines.Select(x => x[0]).ToArray());
            var right = new String(this.lines.Select(x => x[^1]).ToArray());
            ränder = [top, right, bottom, left];
        }

        public bool PasstZuRand(string rand)
        {
            return ränder.Any(x => Check(x, rand));
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
