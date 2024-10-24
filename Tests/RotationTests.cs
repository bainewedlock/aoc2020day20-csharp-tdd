using aoc2020day20_csharp;

namespace Tests
{
    public class RotationTest
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
    }
}
