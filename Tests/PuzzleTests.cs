using aoc2020day20_csharp;

namespace Tests
{
    internal class PuzzleTests
    {
        [Test]
        public void Kann_Alle_Teile_Einlesen()
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
..###..###

Tile 1951:
#.##...##.
#.####...#
.....#..##
#...######
.##.#....#
.###.#####
###.##.##.
.###....#.
..#.#..#.#
#...##.#..

Tile 1171:
####...##.
#..##.#..#
##.#..#.#.
.###.####.
..###.####
.##....##.
.#...####.
#.##.####.
####..#...
.....##...

Tile 1427:
###.##.#..
.#..#.##..
.#.##.#..#
#.#.#.##.#
....#...##
...##..##.
...#.#####
.#.####.#.
..#..###.#
..##.#..#.

Tile 1489:
##.#.#....
..##...#..
.##..##...
..#...#...
#####...#.
#..#.#.#.#
...#.#.#..
##.#...##.
..##.##.##
###.##.#..

Tile 2473:
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.

Tile 2971:
..#.#....#
#...###...
#.#.###...
##.##..#..
.#####..##
.#..####.#
#..#.#..#.
..####.###
..#.#.###.
...#.#.#.#

Tile 2729:
...#.#.#.#
####.#....
..#.#.....
....#..#.#
.##..##.#.
.#.####...
####.#.#..
##.####...
##..#.##..
#.##...##.

Tile 3079:
#.#.#####.
.#..######
..#.......
######....
####.#..#.
.#...#.##.
#.#####.##
..#.###...
..#.......
..#.###...";

            var puzzle = new Puzzle(input);
            var teile = puzzle.Teile;

            Assert.That(teile.Length, Is.EqualTo(9));
            Assert.That(teile.Last().id, Is.EqualTo(3079));
            Assert.That(teile[^2].bottom, Is.EqualTo("#.##...##."));
            Assert.That(puzzle.PuzzleGröße, Is.EqualTo(3));
        }

        [Test]
        public void Löst_einfache_Zeile_schritt_für_Schritt()
        {
            var input = @"
Tile 1:
?AA
?AA
?AA

Tile 2:
ABB
ABB
ABB

Tile 3:
BBC
BBC
BBC

Tile 4:
CCD
CCD
CCD
";

            var p = new Puzzle(input, 4);

            p.SolveStep();
            Assert.That(p.Grid[0, 0]?.id, Is.EqualTo(1), message: "teil 0,0");

            p.SolveStep();
            Assert.That(p.Grid[1, 0]?.id, Is.EqualTo(2), message: "teil 1,0");

            p.SolveStep();
            Assert.That(p.Grid[2, 0]?.id, Is.EqualTo(3), message: "teil 2,0");

            p.SolveStep();
            Assert.That(p.Grid[3, 0]?.id, Is.EqualTo(4), message: "teil 3,0");
        }

        [Test]
        public void Löst_einfache_Zeile_in_einem_Rutsch()
        {
            var input = @"
Tile 1:
?AA
?AA
?AA

Tile 2:
ABB
ABB
ABB

Tile 3:
BBC
BBC
BBC

Tile 4:
CCD
CCD
CCD
";

            var p = new Puzzle(input, 4);

            p.SolveAll();
            Assert.That(p.Grid[0, 0]?.id, Is.EqualTo(1), message: "teil 0,0");
            Assert.That(p.Grid[1, 0]?.id, Is.EqualTo(2), message: "teil 1,0");
            Assert.That(p.Grid[2, 0]?.id, Is.EqualTo(3), message: "teil 2,0");
            Assert.That(p.Grid[3, 0]?.id, Is.EqualTo(4), message: "teil 3,0");
        }


        [Ignore("später")]
        [Test]
        public void Löst_Zeile_mit_Sackgasse()
        {
            var input = @"
Tile 1:
?AA
?AA
?AA

Tile 2:
ABB
ABB
ABB

Tile 3:
BBC
BBC
BBC

Tile 4:
BBB
BBB
BBB
";

            var p = new Puzzle(input, 4);

            p.SolveAll();
            Assert.That(p.Grid[0, 0]?.id, Is.EqualTo(1), message: "teil 0,0");
            Assert.That(p.Grid[1, 0]?.id, Is.EqualTo(2), message: "teil 1,0");
            Assert.That(p.Grid[2, 0]?.id, Is.EqualTo(3), message: "teil 2,0");
            Assert.That(p.Grid[3, 0]?.id, Is.EqualTo(4), message: "teil 3,0");

        }
    }
}
