namespace aoc2020day20_csharp
{
    public class SearchNode
    {
        Puzzle puzzle;
        PuzzleTeil? platziertes_teil = null;
        List<PuzzleTeil> todo_liste = new List<PuzzleTeil>();
        int transform_count = 7;

        public SearchNode(Puzzle p)
        {
            puzzle = p;
            todo_liste = puzzle.ÜbrigeTeile.ToList();
            if (todo_liste.Any()) CanTraverse = true;
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

        void PlatziereTeil()
        {
            platziertes_teil = todo_liste.First();
            puzzle.Platziere_Teil(platziertes_teil);
            todo_liste.RemoveAt(0);
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
                if(!todo_liste.Any()) CanTraverse = false;
                puzzle.Entferne_Teil(platziertes_teil);
                platziertes_teil = null;
            }
        }
    }
}
