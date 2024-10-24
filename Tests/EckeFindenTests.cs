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
    }
}
