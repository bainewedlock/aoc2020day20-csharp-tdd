namespace aoc2020day20_csharp
{
    public class SearchNode
    {
        Puzzle puzzle;
        public PuzzleTeil? PlatziertesTeil {  get; private set; }
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
            if (PlatziertesTeil == null)
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
            PlatziertesTeil = todo_liste.First();
            puzzle.Platziere_Teil(PlatziertesTeil);
            todo_liste.RemoveAt(0);
        }

        void BewegeTeil()
        {
            Console.WriteLine($"bewege teil {PlatziertesTeil.id} ({transform_count})");

            if (transform_count == 4)
            {
                PlatziertesTeil.Flip();
            }
            else
            {
                PlatziertesTeil.Rotate();
            }

            transform_count -= 1;

            if (transform_count == 0)
            {
                if(!todo_liste.Any()) CanTraverse = false;
                puzzle.Entferne_Teil(PlatziertesTeil);
                PlatziertesTeil = null;
            }
        }
    }
}
