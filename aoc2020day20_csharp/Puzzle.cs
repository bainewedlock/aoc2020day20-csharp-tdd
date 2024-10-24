using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2020day20_csharp
{
    public class Puzzle
    {
        public Puzzle(string input)
        {
            var blocks = input.Trim().Split("\r\n\r\n");
            Teile = blocks.Select(x => new PuzzleTeil(x)).ToArray();
        }
    
        public PuzzleTeil[] Teile;

        public PuzzleTeil FindeIrgendEineEcke()
        {
            return null;
        }
    }
}
