namespace aoc2020day20_csharp
{
    public class SearchNode
    {
        Puzzle puzzle;
        public PuzzleTeil? PlatziertesTeil {  get; private set; }
        List<PuzzleTeil> todo_liste = new List<PuzzleTeil>();
        int step = 0;

        public SearchNode(Puzzle p)
        {
            puzzle = p;
            todo_liste = puzzle.ÜbrigeTeile.ToList();
            if (todo_liste.Any()) CanTraverse = true;
        }

        public bool CanTraverse { get; private set; }

        public SearchNode Traverse()
        {
            step += 1;

            if (step == 1)
            {
                PlatziereTeil();
            }
            else if (step == 2 || step == 3 || step == 4)
            {
                PlatziertesTeil.Rotate();
            }
            else if (step == 5)
            {
                PlatziertesTeil.Flip();
            }
            else if (step == 6 || step == 7 || step == 8)
            {
                PlatziertesTeil.Rotate();
            }
            else if (step == 9)
            {
                EntferneTeil();
                step = 0;
            }

            return new SearchNode(puzzle);
        }

        void PlatziereTeil()
        {
            PlatziertesTeil = todo_liste.First();
            puzzle.Platziere_Teil(PlatziertesTeil);
            todo_liste.RemoveAt(0);
        }

        void EntferneTeil()
        {
            puzzle.Entferne_Teil(PlatziertesTeil);
            PlatziertesTeil = null;
            if (!todo_liste.Any()) CanTraverse = false;
        }
    }
}
