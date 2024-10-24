using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc2020day20_csharp;

namespace Tests
{
    public class EckeFindenTests
    {
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
