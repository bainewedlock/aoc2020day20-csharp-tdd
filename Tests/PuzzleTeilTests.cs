using aoc2020day20_csharp;

namespace Tests
{
    public class PuzzleTeilTests
    { 
        [Test]
        public void Teil_Kann_sich_um_90_grad_rotieren()
        {
            var p = new PuzzleTeil(@"
                Tile 1:
                123
                345
                678");

            p.Rotate();

            CollectionAssert.AreEqual( new[]
                {"631",
                 "742",
                 "853"}, p.lines);

            Assert.That(p.top, Is.EqualTo("631"));
            Assert.That(p.right, Is.EqualTo("123"));
            Assert.That(p.bottom  , Is.EqualTo("853"));
            Assert.That(p.left, Is.EqualTo("678"));
        }

        [Test]
        public void Kann_Teil_Einlesen()
        {
            var input = @"Tile 2311:
                          ..##.#..#.
                          ##..#.....
                          #...##..#.
                          ####.#...#
                          ##.##.###.
                          ##...#.###
                          .#.#.#..##
                          ..#....#..
                          ###...#.#.
                          ..###..###";

            var teil = new PuzzleTeil(input);

            Assert.That(teil.id, Is.EqualTo(2311));
            CollectionAssert.AreEqual(new[] {
              "..##.#..#.",
              "##..#.....",
              "#...##..#.",
              "####.#...#",
              "##.##.###.",
              "##...#.###",
              ".#.#.#..##",
              "..#....#..",
              "###...#.#.",
              "..###..###"}, teil.lines);
            Assert.That(teil.top, Is.EqualTo("..##.#..#."));
            Assert.That(teil.bottom, Is.EqualTo("..###..###"));
            Assert.That(teil.left, Is.EqualTo(".#####..#."));
            Assert.That(teil.right, Is.EqualTo("...#.##..#"));
        }

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
        public void Flip()
        {
            var teil = new PuzzleTeil(@"Tile 1:
                                        123
                                        456
                                        789");
            teil.Flip();
            CollectionAssert.AreEqual(teil.lines, new[] {
                                       "321",
                                       "654",
                                       "987"
            });

        }
    }
}
