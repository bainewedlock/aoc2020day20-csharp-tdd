using System.Linq.Expressions;

namespace aoc2020day20_csharp
{
    public class PuzzleTeil
    {
        public int id;
        public string[] lines;
        public string[] ränder;
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

        public PuzzleTeil(PuzzleTeil o)
        {
            id = o.id;
            lines = o.lines.ToArray();
            ränder = o.ränder.ToArray();
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

        public void Rotate()
        {
            var n = lines.Length;
            var ls = new List<string>();
            for (int y=0; y<n; y++)
            {
                ls.Add("");
                for(int x=0; x<n; x++)
                {
                    ls[^1] += lines[n-x-1][y];
                }
            }
            lines = ls.ToArray();


            var rotated = ränder.Take(3).ToList();
            rotated.Insert(0, ränder.Last());
            rotated[0] = Rev(rotated[0]);
            rotated[2] = Rev(rotated[2]);
            ränder = rotated.ToArray();
            
        }

        public bool PasstInEcke(string rand1, string rand2)
        {
            return string.Join("|", ränder.Concat(ränder)).Contains($"{rand1}|{rand2}");
        }

        public override string ToString()
        {
            return $"{id}";
        }

        public void Flip()
        {
            for (int i=0; i<lines.Length; i++)
            {
                lines[i] = Rev(lines[i]);
            }
            ränder[0] = Rev(ränder[0]);
            ränder[2] = Rev(ränder[2]);
        }

        public PuzzleTeil Copy()
        {
            return new PuzzleTeil(this);
        }
    }
}
