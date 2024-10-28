using aoc2020day20_csharp;

namespace Tests
{
    public class SearchNodeTests
    {
        [Test]
        public void Node_ohne_keine_Optionen__wenn_es_keine_übrigen_Teile_gibt()
        {
            var p = new Puzzle(@"Tile 1:
                                 AAA
                                 AAA
                                 AAA");
            p.ÜbrigeTeile.Clear();

            var n = new SearchNode(p);

            Assert.False(n.CanTraverse);
        }

        [Test]
        public void Traverse_platziert_erstbestes_Teil_links_oben_und_liefert_neue_Node()
        {
            var p = new Puzzle(@"Tile 123:
                                 AAA
                                 AAA
                                 AAA");

            var n = new SearchNode(p);

            Assert.True(n.CanTraverse);

            var n2 = n.Traverse();

            CollectionAssert.IsEmpty(p.ÜbrigeTeile);

            Assert.That(p.Grid[0, 0].id, Is.EqualTo(123));

            Assert.False(n2.CanTraverse);
            Assert.That(n, Is.Not.EqualTo(n2));
        }

        [Test]
        public void Platziert_immer_Teil_auf_nächstes_Feld_in_Leserichtung()
        {
            var p = new Puzzle(@"Tile 1:
                                 A

                                 Tile 2:
                                 B

                                 Tile 3:
                                 C

                                 Tile 4:
                                 D
                                 ");

            var n = new SearchNode(p);

            n = n.Traverse();
            Assert.That(p.Grid[0, 0].id, Is.EqualTo(1));

            n = n.Traverse();
            Assert.That(p.Grid[1, 0].id, Is.EqualTo(2));

            n = n.Traverse();
            Assert.That(p.Grid[0, 1].id, Is.EqualTo(3));
            
            n.Traverse();
            Assert.That(p.Grid[1, 1].id, Is.EqualTo(4));
        }

        [Test]
        public void Traverse_probiert_alle_Puzzle_Stellungen_aus()
        {
            var p = new Puzzle(@"Tile 123:
                                 12
                                 34");

            var n = new SearchNode(p);
            n.Traverse();
            var t = p.Grid[0, 0];
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "12",
                                      "34" });

            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "31",
                                      "42" });

            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "43",
                                      "21" });

            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "24",
                                      "13" });

            n.Traverse(); // flip
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "42",
                                      "31" });

            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "34",
                                      "12" });

            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "13",
                                      "24" });

            Assert.True(n.CanTraverse);
            n.Traverse();
            CollectionAssert.AreEqual(t.lines, new[] {
                                      "21",
                                      "43" });

            Assert.False(n.CanTraverse);
        }


    }
}
