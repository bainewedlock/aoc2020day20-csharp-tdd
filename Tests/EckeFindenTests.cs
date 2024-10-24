using aoc2020day20_csharp;

namespace Tests
{
    public class EckeFindenTests
    {
        [TestCase("#..", TestName = "Rand_passt_zu_Teil (oben)")]
        [TestCase("..#", TestName = "Rand_passt_zu_Teil (oben geflippt)")]
        [TestCase("#.#", TestName = "Rand_passt_zu_Teil (links)")]
        [TestCase(".#.", TestName = "Rand_passt_zu_Teil (rechts)")]
        [TestCase("##.", TestName = "Rand_passt_zu_Teil (unten)")]
        [TestCase(".##", TestName = "Rand_passt_zu_Teil (unten geflippt)")]
        public void Rand_passt_zu_Teil(string rand)
        {
            var input = @"
Tile 1:
#..
..#
##.";

            var teil = new PuzzleTeil(input);

            Assert.That(teil.PasstZuRand(rand), Is.True);
        }



        [Test]
        public void Simple()
        {
            var input = @"
Tile 1:
#..
...
...

Tile 2:
###
#..
##.

Tile 3:
##.
...
...
";
            var p = new Puzzle(input);
            var eck = p.FindeIrgendEineEcke();
            Assert.That(eck.id, Is.EqualTo(2));
        }

        [Test]
        public void Kann_Einzelne_Teile_Finden()
        {
            var input = @"
Tile 1:
22A
2.A
BBA

Tile 2:
A11
A.1
ACC

Tile 3:
BBA
3.D
33D

Tile 4:
ACC
D.4
D44
";

            var p = new Puzzle(input);

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[0, 0]?.id, Is.EqualTo(1), message:"ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[1, 0]?.id, Is.EqualTo(2), message:"teil rechts von der ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[0, 1]?.id, Is.EqualTo(3), message:"teil unter der ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[1, 1]?.id, Is.EqualTo(4), message:"teil unten rechts von der ecke passt nicht");
        }

        [Test]
        public void Kann_Einzelne_Teile_Finden_und_rotieren()
        {
            var input = @"
Tile 1:
c..
...
.vv

Tile 2:
yxa
z.b
..c

Tile 3:
.d.
z..
y3.

Tile 4:
.d.
4bv
44v
";

            var p = new Puzzle(input);

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[0, 0]?.id, Is.EqualTo(2), message:"ecke passt nicht");

            CollectionAssert.AreEqual(p.Lösung[0, 0].lines, new[]
            {
                "abc",
                "x..",
                "yz."
            }, "ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[1, 0]?.id, Is.EqualTo(1), message:"teil rechts von der ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[0, 1]?.id, Is.EqualTo(3), message:"teil unter der ecke passt nicht");

            p.FindeNächstesTeil();
            Assert.That(p.Lösung[1, 1]?.id, Is.EqualTo(4), message:"teil unten rechts von der ecke passt nicht");
        }

        [Test]
        public void Kann_Alle_Teile_Finden()
        {
            var input = @"
Tile 1:
22A
2.A
BBA

Tile 2:
A11
A.1
ACC

Tile 3:
BBA
3.D
33D

Tile 4:
ACC
D.4
D44
";

            var p = new Puzzle(input);

            p.FindeAlleTeile();
            Assert.That(p.Lösung[0, 0]?.id, Is.EqualTo(1), message:"ecke passt nicht");
            Assert.That(p.Lösung[1, 0]?.id, Is.EqualTo(2), message:"teil rechts von der ecke passt nicht");
            Assert.That(p.Lösung[0, 1]?.id, Is.EqualTo(3), message:"teil unter der ecke passt nicht");
            Assert.That(p.Lösung[1, 1]?.id, Is.EqualTo(4), message:"teil unten rechts von der ecke passt nicht");
        }
    }
}
