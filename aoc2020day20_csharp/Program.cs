namespace aoc2020day20_csharp
{
    public class Program
    {
        public static string Foo()
        {
            return "bar";
        }

        static void Main(string[] args)
        {
            var p = new Puzzle(File.ReadAllText("input.txt"));
            p.FindeAlleTeile();


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
