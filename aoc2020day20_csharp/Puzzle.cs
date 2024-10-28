using System.Text;

namespace aoc2020day20_csharp
{
    public class Puzzle
    {
        public PuzzleTeil[] Teile;
        public PuzzleTeil[,] Grid;
        public HashSet<PuzzleTeil> ÜbrigeTeile = new HashSet<PuzzleTeil>();
        public int PuzzleGröße;

        public Puzzle(string input, int? größe = null)
        {
            var blocks = input.Trim().Split("\r\n\r\n");
            Teile = blocks.Select(x => new PuzzleTeil(x)).ToArray();
            ÜbrigeTeile = new HashSet<PuzzleTeil>(Teile);
            PuzzleGröße = größe ?? (int)Math.Sqrt(Teile.Length);
            Grid = new PuzzleTeil[PuzzleGröße, PuzzleGröße];
        }

        public bool IstEckteil(PuzzleTeil kandidat)
        {
            var andere = Teile.Where(x => x.id != kandidat.id).ToArray();
            var count = 0;
            if (!MatchesAny(kandidat.top, andere)) count++;
            if (!MatchesAny(kandidat.right, andere)) count++;
            if (!MatchesAny(kandidat.bottom, andere)) count++;
            if (!MatchesAny(kandidat.left, andere)) count++;

            return count >= 2;
        }

        PuzzleTeil[] GetMatches(string rand, IEnumerable<PuzzleTeil> teile)
        {
            return teile.Where(x => x.PasstZuRand(rand)).ToArray();
        }

        bool MatchesAny(string rand, IEnumerable<PuzzleTeil> teile)
        {
            return GetMatches(rand, teile).Any();
        }

        public void Platziere_Teil(PuzzleTeil teil)
        {
            if (!ÜbrigeTeile.Contains(teil))
                throw new ArgumentException("das ist kein übriges Teil!");

            var (x, y) = FindeFreiePos();
            Grid[x, y] = teil;
            if (!ÜbrigeTeile.Remove(teil))
                throw new ApplicationException("unerwartet");
        }

        public void Entferne_Teil(PuzzleTeil teil)
        {
            for (int y = 0; y < PuzzleGröße; y++)
            for (int x = 0; x < PuzzleGröße; x++)
            if (Grid[x, y] == teil)
            {
                Grid[x, y] = null;
                ÜbrigeTeile.Add(teil);
                return;
            }

            throw new ApplicationException("unerwartet");
        }

        (int x, int y) FindeTeil(PuzzleTeil teil)
        {
            for (int y = 0; y < PuzzleGröße; y++)
            for (int x = 0; x < PuzzleGröße; x++)
            if (Grid[x, y] == teil)
                return (x, y);
            throw new InvalidOperationException("pos nicht gefunden");
        }

        (int x, int y) FindeFreiePos()
        {
            return FindeTeil(null);
        }

        public bool Teil_passt(PuzzleTeil teil)
        {
            var (x,y) = FindeTeil(teil);

            string rand_oben = "";
            string rand_links = "";

            if (x == 0) // Spalte 0
            {
                if (y == 0) // Ecke Links Oben
                {
                    return IstEckteil(teil);
                }
                else // Rest von Spalte 0
                {
                    rand_oben = Grid[x, y-1].bottom;
                }
            }
            else // ab Spalte 1
            {
                rand_links = Grid[x - 1, y].right;

                if (y > 0) // ab Reihe 1
                {
                    rand_oben = Grid[x, y - 1].bottom;
                }
            }

            return teil.top.StartsWith(rand_oben) &&
                   teil.left.StartsWith(rand_links);
        }

        Stack<SearchNode> search_nodes = new Stack<SearchNode>();

        public bool SolveStep(Action callback = null)
        {
            if (!search_nodes.Any())
            {
                search_nodes.Push(new SearchNode(this));
            }

            var n = search_nodes.First();

            if (n.CanTraverse)
            {
                var n2 = n.Traverse();
                if (n.PlatziertesTeil != null && Teil_passt(n.PlatziertesTeil))
                {
                    if (callback != null) callback();

                    if (!ÜbrigeTeile.Any())
                    {
                        return true;
                    }
                    else
                    {
                        search_nodes.Push(n2);
                    }
                }
            }
            else
            {
                search_nodes.Pop();
            }
            return false;
        }

        public string[] ToLines()
        {
            var n = GetTeilGröße();
            var lines = new List<string>();

            for (int py=0; py<PuzzleGröße; py++)
            {
                for (int y = 0; y < n; y++)
                {
                    var sb = new StringBuilder();
                    for (var px=0; px<PuzzleGröße; px++)
                    {
                        var t = Grid[px, py];
                        if (t == null)
                        {
                            sb.Append(new String('.', n));
                        }
                        else
                        {
                            sb.Append(t.lines[y]);
                        }
                    }
                    lines.Add(sb.ToString());
                }
            }
            return lines.ToArray();
        }

        public void PrintPuzzle()
        {
            for (int py = 0; py < PuzzleGröße; py++)
            {
                var ids = new List<int>();
                for (var px = 0; px < PuzzleGröße; px++)
                {
                    ids.Add(Grid[px, py]?.id ?? 0);
                }
                Console.WriteLine(String.Join(" ", ids));
            }

            foreach(var line in ToLines())
            {
                Console.WriteLine(line);
            }
        }

        int GetTeilGröße()
        {
            return (Grid[0, 0] ?? ÜbrigeTeile.First()).top.Length;
        }
    }
}
