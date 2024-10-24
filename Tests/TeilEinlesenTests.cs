using aoc2020day20_csharp;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
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
    }
}