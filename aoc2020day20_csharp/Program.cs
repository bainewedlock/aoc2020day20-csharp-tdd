using System.Drawing;
using AnimatedGif;
using AniGif = AnimatedGif.AnimatedGif;

namespace aoc2020day20_csharp
{
    public class Program
    {
        const string gif_name = "puzzle.gif";

        static void Main(string[] args)
        {
            var p = new Puzzle(File.ReadAllText("input.txt"));

            using (var gif = AniGif.Create(gif_name, 33))
            {
                var lines = p.ToLines();

                while (!p.SolveStep(() => AddFrame(gif, p, -1)))
                {
                }

                // das Ende der Animation deutlich länger anzeigen
                AddFrame(gif, p, 2000);
            }

            Console.WriteLine($"{gif_name} written.");

            // Puzzle Lösung berechnen
            //p.PrintPuzzle();
            var u = p.Grid.GetLength(0) - 1;
            long a = p.Grid[0, 0].id;
            long b = p.Grid[u, 0].id;
            long c = p.Grid[u, u].id;
            long d = p.Grid[0, u].id;
            Console.WriteLine($"part 1 : {a * b * c * d}");
            Console.WriteLine("66020135789767 is correct");
        }

        static void AddFrame(AnimatedGifCreator gif, Puzzle puzzle, int delay)
        {
            var lines = puzzle.ToLines();
            var w = lines[0].Length;
            var h = lines.Length;
            var bmp = new Bitmap(w, h);
            for (int y=0; y < h; y++)
            for (int x=0; x < w; x++)
                bmp.SetPixel(x, y, lines[y][x]=='#' ? Color.White:Color.Black);
            gif.AddFrame(bmp, delay: delay, quality: GifQuality.Bit8);
        }

    }
}
