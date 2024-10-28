namespace aoc2020day20_csharp
{
    public class SearchNode
    {
        Puzzle puzzle;
        PuzzleTeil? platziertes_teil = null;
        int transform_count = 7;

        public SearchNode(Puzzle p)
        {
            puzzle = p;
            if (puzzle.ÜbrigeTeile.Any()) CanTraverse = true;
        }

        public bool CanTraverse { get; private set; }

        public SearchNode Traverse()
        {
            if (platziertes_teil == null)
            {
                PlatziereTeil();
            }
            else if (transform_count > 0)
            {
                BewegeTeil();
            }
            else
            {
                throw new InvalidOperationException("cant traverse");
            }

            return new SearchNode(puzzle);
        }

        (int x, int y) FindNextPos()
        {
            for (int y = 0; y < puzzle.PuzzleGröße; y++)
                for (int x = 0; x < puzzle.PuzzleGröße; x++)
                    if (puzzle.Grid[x, y] == null)
                        return (x, y);
            return (-1, -1);
        }

        void PlatziereTeil()
        {
            var (x, y) = FindNextPos();
            platziertes_teil = puzzle.ÜbrigeTeile.First();
            puzzle.Grid[x, y] = platziertes_teil;
            puzzle.ÜbrigeTeile.Remove(platziertes_teil);
        }

        void BewegeTeil()
        {
            if (transform_count == 4)
            {
                platziertes_teil.Flip();
            }
            else
            {
                platziertes_teil.Rotate();
            }

            transform_count -= 1;

            if (transform_count == 0)
            {
                CanTraverse = false;
            }
        }
    }
}
