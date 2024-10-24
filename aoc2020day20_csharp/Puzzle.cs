using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace aoc2020day20_csharp
{
    public class Puzzle
    {
        public PuzzleTeil[,] Lösung = new PuzzleTeil[3, 3];
        public HashSet<PuzzleTeil> ÜbrigeTeile = new HashSet<PuzzleTeil>();
        public int PuzzleGröße;

        public Puzzle(string input)
        {
            var blocks = input.Trim().Split("\r\n\r\n");
            Teile = blocks.Select(x => new PuzzleTeil(x)).ToArray();
            ÜbrigeTeile = new HashSet<PuzzleTeil>(Teile);
            PuzzleGröße = (int)Math.Sqrt(Teile.Length);
        }

        public PuzzleTeil[] Teile;

        public PuzzleTeil FindeIrgendEineEcke()
        {
            for (int i = 0; i < Teile.Length; i++)
            {
                var kandidat = Teile[i];
                var andere = Teile.Except([kandidat]).ToArray();
                var count = 0;
                if (MatchesAny(kandidat.top, andere)) count++;
                if (MatchesAny(kandidat.right, andere)) count++;
                if (MatchesAny(kandidat.bottom, andere)) count++;
                if (MatchesAny(kandidat.left, andere)) count++;

                if (count == 2) return kandidat;
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
                    while (MatchesAny(ecke.top, ÜbrigeTeile) ||
                           MatchesAny(ecke.left, ÜbrigeTeile))
                    {
                        ecke.Rotate();
                    }
                    Lösung[0, 0] = ecke;
                    return;
                }
                var teil_darüber = Lösung[x, y - 1];
                var rand = teil_darüber.bottom;
                var teil = GetMatches(rand, ÜbrigeTeile).Single();
                ÜbrigeTeile.Remove(teil);
                while (!teil_darüber.PasstZuRand(teil.top))
                    teil.Rotate();
                Lösung[x, y] = teil;
                return;
            }
            else // ab Spalte 1
            {
                if (y == 0) // Reihe 0
                {
                    var teil_links_davon = Lösung[x - 1, y];
                    var rand = teil_links_davon.right;
                    var teil = GetMatches(rand, ÜbrigeTeile).Single();
                    Lösung[x, y] = teil;
                    ÜbrigeTeile.Remove(teil);
                    return;
                }
                else // ab Reihe 1
                {
                    var teil_links_davon = Lösung[x - 1, y];
                    var rand1 = teil_links_davon.right;
                    var teil_darüber = Lösung[x, y - 1];
                    var rand2 = teil_darüber.bottom;

                    var teil = ÜbrigeTeile.Single(x => x.PasstInEcke(rand1, rand2));
                    ÜbrigeTeile.Remove(teil);

                    while (teil.left != rand1 || teil.top != rand2)
                        teil.Rotate();

                    Lösung[x, y] = teil;
                    return;
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
    }
}
