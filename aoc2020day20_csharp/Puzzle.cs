namespace aoc2020day20_csharp
{
    public class Puzzle
    {
        public Puzzle(string input)
        {
            var blocks = input.Trim().Split("\r\n\r\n");
            Teile = blocks.Select(x => new PuzzleTeil(x)).ToArray();
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

        bool MatchesAny(string rand, PuzzleTeil[] teile)
        {
            return teile.Any(x => x.PasstZuRand(rand));
        }
    }
}
