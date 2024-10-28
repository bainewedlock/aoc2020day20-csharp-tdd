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
        public void Traverse_probiert_alle_Möglichkeiten_ein_Teil_zu_positionieren()
        {
            var p = new Puzzle(@"Tile 123:
                                 12
                                 34");

            var n = new SearchNode(p);
            n.Traverse();
            var t = p.Grid[0, 0];
            CollectionAssert.AreEqual(new[] {
                                      "12",
                                      "34" }, t.lines);

            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "31",
                                      "42" }, t.lines);

            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "43",
                                      "21" }, t.lines);

            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "24",
                                      "13" }, t.lines);

            n.Traverse(); // flip
            CollectionAssert.AreEqual(new[] {
                                      "42",
                                      "31" }, t.lines);

            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "34",
                                      "12" }, t.lines);

            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "13",
                                      "24" }, t.lines);

            Assert.True(n.CanTraverse);
            n.Traverse();
            CollectionAssert.AreEqual(new[] {
                                      "21",
                                      "43" }, t.lines);

            Assert.False(n.CanTraverse);
        }

        [Test]
        public void Traverse_probiert_weiteres_Teil_falls_möglich()
        {
            var p = new Puzzle(@"Tile 123:
                                 12
                                 34

                                 Tile 456:
                                 56
                                 78", 2);

            var n = new SearchNode(p);
            for (int i = 0; i < 8; i++) n.Traverse();

            Assert.True(n.CanTraverse);
            n.Traverse();

            CollectionAssert.AreEqual(new[]
            {
                "56",
                "78"
            }, p.Grid[0,0].lines);
        }


    }
}
