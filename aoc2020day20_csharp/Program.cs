namespace aoc2020day20_csharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var p = new Puzzle(File.ReadAllText("input.txt"));
            p.DebugRänder();

            while (!p.SolveStep())
            {

            }

            var u = p.Grid.GetLength(0)-1;
            long a = p.Grid[0, 0].id;
            long b = p.Grid[u, 0].id;
            long c = p.Grid[u, u].id;
            long d = p.Grid[0, u].id;

            Console.WriteLine($"part 1 : {a*b*c*d}");
            Console.WriteLine("66020135789767 is correct");


            //using (var gif = AnimatedGif.AnimatedGif.Create("mygif.gif", 33))
            //{
            //    var bmp = new Bitmap(100, 100);

            //    for(int i = 0; i <100; i++)
            //    {
            //        bmp.SetPixel(i, i, Color.Blue);
            //        gif.AddFrame(bmp, delay: -1, quality: GifQuality.Bit8);
            //    }
            //}
        }
    }
}
