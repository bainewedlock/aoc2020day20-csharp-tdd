using System.Drawing;
using AnimatedGif;
using AniGif = AnimatedGif.AnimatedGif;

namespace aoc2020day20_csharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var p = new Puzzle(File.ReadAllText("input.txt"));
            p.DebugRänder();

            var gif_name = "puzzle.gif";

            using (var gif = AniGif.Create(gif_name, 33))
            {
                var lines = p.ToLines();
                var bmp = new Bitmap(lines[0].Length, lines.Length);

                void add_frame(int delay)
                {
                    lines = p.ToLines();
                    for (int y=0; y < lines.Length; y++)
                    {
                        var line = lines[y];
                        for (int x=0; x < line.Length; x++)
                        {
                            if (line[x] == '#')
                                bmp.SetPixel(x, y, Color.White);
                            else
                                bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                    gif.AddFrame(bmp, delay: delay, quality: GifQuality.Bit8);
                }

                while (!p.SolveStep(() => add_frame(-1)))
                {
                }

                add_frame(2000);
            }

            Console.WriteLine($"{gif_name} written.");

            var u = p.Grid.GetLength(0) - 1;
            long a = p.Grid[0, 0].id;
            long b = p.Grid[u, 0].id;
            long c = p.Grid[u, u].id;
            long d = p.Grid[0, u].id;

            Console.WriteLine($"part 1 : {a * b * c * d}");
            Console.WriteLine("66020135789767 is correct");
        }
    }
}
