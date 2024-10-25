using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace aoc2020day20_csharp
{
    public class Puzzle
    {
        public PuzzleTeil[,] Lösung;
        public HashSet<PuzzleTeil> ÜbrigeTeile = new HashSet<PuzzleTeil>();
        public int PuzzleGröße;

        public Puzzle(string input, int? größe = null)
        {
            var blocks = input.Trim().Split("\r\n\r\n");
            Teile = blocks.Select(x => new PuzzleTeil(x)).ToArray();
            ÜbrigeTeile = new HashSet<PuzzleTeil>(Teile);
            PuzzleGröße = größe ?? (int)Math.Sqrt(Teile.Length);
            Lösung = new PuzzleTeil[PuzzleGröße, PuzzleGröße];
        }

        public PuzzleTeil[] Teile;

        public PuzzleTeil FindeIrgendEineEcke()
        {
            for (int i = 0; i < Teile.Length; i++)
            {
                var kandidat = Teile[i];
                var andere = Teile.Except([kandidat]).ToArray();
                var count = 0;
                if (!MatchesAny(kandidat.top, andere)) count++;
                if (!MatchesAny(kandidat.right, andere)) count++;
                if (!MatchesAny(kandidat.bottom, andere)) count++;
                if (!MatchesAny(kandidat.left, andere)) count++;

                if (count >= 2) return kandidat;
            }
            throw new InvalidOperationException("noob");
        }

        PuzzleTeil[] GetMatches(string rand, IEnumerable<PuzzleTeil> teile)
        {
            return teile.Where(x => x.PasstZuRand(rand)).ToArray();
        }

        bool MatchesAny(string rand, IEnumerable<PuzzleTeil> teile)
        {
            return GetMatches(rand, teile).Any();
        }

        (bool found, int x, int y) FindNextPos()
        {
            for (int y = 0; y < PuzzleGröße; y++)
                for (int x = 0; x < PuzzleGröße; x++)
                    if (Lösung[x, y] == null)
                        return (false, x, y);
            return (true, 0, 0);
        }

        public void FindeNächstesTeil()
        {
            var (done, x, y) = FindNextPos();
            if (done) return;

            if (x == 0) // Spalte 0
            {
                if (y == 0) // Ecke Links Oben
                {
                    var ecke = FindeIrgendEineEcke();
                    ÜbrigeTeile.Remove(ecke);
                    //while (MatchesAny(ecke.top, ÜbrigeTeile) ||
                    //       MatchesAny(ecke.left, ÜbrigeTeile))
                    //{
                    //    ecke.Rotate();
                    //}
                    Lösung[0, 0] = ecke;
                }
                else // Rest von Spalte 0
                {
                    var teil_darüber = Lösung[x, y - 1];

                    var rand = teil_darüber.bottom;
                    //var teil = GetMatches(rand, ÜbrigeTeile).Single();
                    var teil = ÜbrigeTeile.First(x =>
                        x.ränder.Any(r => teil_darüber.PasstZuRand(r)));
                    ÜbrigeTeile.Remove(teil);
                    //while (!teil_darüber.PasstZuRand(teil.top))
                    //    teil.Rotate();
                    Lösung[x, y] = teil;
                }
            }
            else // ab Spalte 1
            {
                if (y == 0) // Reihe 0
                {
                    var teil_links_davon = Lösung[x - 1, y];
                    var rand = teil_links_davon.right;
                    var teil = ÜbrigeTeile.First(x =>
                        x.ränder.Any(r => teil_links_davon.PasstZuRand(r)));
                    Lösung[x, y] = teil;
                    ÜbrigeTeile.Remove(teil);
                }
                else // ab Reihe 1
                {
                    var teil_links_davon = Lösung[x - 1, y];
                    var rand1 = teil_links_davon.right;
                    var teil_darüber = Lösung[x, y - 1];
                    var rand2 = teil_darüber.bottom;

                    var teil = ÜbrigeTeile.Single(x =>
                        x.ränder.Any(r => teil_links_davon.PasstZuRand(r)));
                    ÜbrigeTeile.Remove(teil);

                    //while (teil.left != rand1 || teil.top != rand2)
                    //    teil.Rotate();

                    Lösung[x, y] = teil;
                }
            }
        }

        public void FindeAlleTeile()
        {
            while (ÜbrigeTeile.Any())
            {
                FindeNächstesTeil();
            }
        }

        public void DebugRänder()
        {
            for(int i=0; i<Teile.Length; i++)
            {
                var t1 = Teile[i];
                var passend = new List<string>();
                for(int j=0; j<Teile.Length; j++)
                {
                    if (i == j) continue;
                    var t2 = Teile[j];

                    foreach(var r in t2.ränder)
                    {
                        if (t1.PasstZuRand(r))
                        {
                            passend.Add(r);
                        }
                    }
                }
                var doppelte =
                        passend.GroupBy(p => p)
                               .Where(g => g.Count() > 1)
                               .Select(g => g.Key)
                               .ToArray();
                foreach(var d in doppelte)
                {
                    Console.WriteLine($"{t1.id} rand passt mehrfach: {d}");

                }
            }
        }
    }
}
