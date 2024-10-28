namespace aoc2020day20_csharp
{
    public class SearchNode
    {
        Puzzle puzzle;
        public PuzzleTeil? PlatziertesTeil {  get; private set; }
        List<PuzzleTeil> todo_liste = new List<PuzzleTeil>();
        IEnumerator<bool> steps;
        public bool CanTraverse { get; private set; }

        public SearchNode(Puzzle p)
        {
            puzzle = p;
            todo_liste = p.ÜbrigeTeile.ToList();
            if (todo_liste.Any()) CanTraverse = true;
            steps = StepSequence().GetEnumerator();
        }

        public SearchNode Traverse()
        {
            steps.MoveNext();
            return new SearchNode(puzzle);
        }

        IEnumerable<bool> StepSequence()
        {
            PlatziereTeil();
            yield return true;

            for (int i = 0; i < 3; i++)
            {
                PlatziertesTeil.Rotate();
                yield return true;
            }

            PlatziertesTeil.Flip();
            yield return true;

            for (int i = 0; i < 3; i++)
            {
                PlatziertesTeil.Rotate();
                yield return true;
            }

            EntferneTeil();
            steps = StepSequence().GetEnumerator();
            yield return true;
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
