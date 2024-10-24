using System.Runtime.InteropServices;

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

        public void FindeNächstesTeil()
        {
            for (int y = 0; y < PuzzleGröße; y++)
                for (int x = 0; x < PuzzleGröße; x++)
            {
                if (Lösung[x, y] == null)
                {
                    if (x == 0) // erstes Teil einer Reihe
                    {
                        if (y == 0) // linke obere ecke
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
                    else
                    {
                        if (y == 0) // obere reihe
                        {
                            var teil_links_davon = Lösung[x - 1, y];
                            var rand = teil_links_davon.right;
                            var teil = GetMatches(rand, ÜbrigeTeile).Single();
                            Lösung[x, y] = teil;
                            ÜbrigeTeile.Remove(teil);
                            return;
                        }
                        else
                        {
                            var teil_links_davon = Lösung[x - 1, y];
                            var rand1 = teil_links_davon.right;
                            var teil_darüber = Lösung[x, y - 1];
                            var rand2 = teil_darüber.bottom;

                            //var teil = GetMatches(rand1, ÜbrigeTeile).Single();

                            var teil = ÜbrigeTeile.Single(x => x.PasstInEcke(rand1, rand2));
                            ÜbrigeTeile.Remove(teil);

                            while (teil.left != rand1 || teil.top != rand2)
                                teil.Rotate();


                            //var teil = ÜbrigeTeile.Single(x => x.left == rand1
                            //                            && x.top  == rand2);

                            Lösung[x, y] = teil;
                            return;
                        }
                    }
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
