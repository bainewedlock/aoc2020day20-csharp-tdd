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
            for (int i=0; i<Teile.Length; i++)
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
            if (Lösung[0,0]==null)
            {
                var ecke = FindeIrgendEineEcke();
                ÜbrigeTeile.Remove(ecke);
                // richtig hindrehen
                while (MatchesAny(ecke.top, ÜbrigeTeile) ||
                       MatchesAny(ecke.left, ÜbrigeTeile))
                {
                    ecke.Rotate();
                }
                Lösung[0, 0] = ecke;
            }
            else
            {
                for(int y=0; y<PuzzleGröße; y++)
                for(int x=0; x<PuzzleGröße; x++)
                {
                    if (Lösung[x, y] == null)
                    {
                        var vorgänger = x == 0 ? Lösung[x, y - 1] : Lösung[x-1, y];
                        var rand = x == 0 ? vorgänger.bottom : vorgänger.right;
                        var matches = GetMatches(rand, ÜbrigeTeile);

                        if (matches.Length != 1)
                            throw new InvalidOperationException(
                                $"unexpected match count: {matches.Length}");

                        Lösung[x, y] = matches[0];
                        ÜbrigeTeile.Remove(matches[0]);
                        return;
                    }
                }
            }
        }

        public void FindeAlleTeile()
        {
            while(ÜbrigeTeile.Any())
            {
                FindeNächstesTeil();
            }
        }
    }
}
