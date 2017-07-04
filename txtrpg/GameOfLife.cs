using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace txtrpg
{
    public static class GameOfLife
    {
        public static void Start()
        {

            var console = new ConsoleExt();
            var rnd = new Random(45156);
            var w = console.WindowWidth;
            var h = console.WindowHeight;
            /*
            console.Write(21, 5, '*');
            console.Write(22, 5, '*');
            console.Write(23, 5, '*');
            console.Write(23, 4, '*');
            console.Write(22, 3, '*');
            */

            var sw = new Stopwatch();
            while (!Console.KeyAvailable)
            {
                // init random game     
                console.Clear();
                for (var y = 0; y < h; y++)
                {
                    for (var x = 0; x < w; x++)
                    {
                        bool alive = (rnd.NextDouble() > 0.85);
                        if (alive) console.Write(x, y, '*');
                    }
                }

                // run game
                for (var i = 0; i < 500; i++)
                {
                    sw.Reset();
                    sw.Start();
                    var buff = new char[w, h];
                    for (var y = 0; y < h; y++)
                    {
                        for (var x = 0; x < w; x++)
                        {
                            var cell = console.Read(x, y);
                            var alive = (cell == '*');
                            var neighbors = new char[] { console.Read(x - 1, y), console.Read(x, y - 1), console.Read(x + 1, y), console.Read(x, y + 1),
                            console.Read(x - 1, y - 1), console.Read(x + 1, y + 1), console.Read(x + 1, y - 1), console.Read(x - 1, y + 1) };
                            var liveNeighbors = neighbors.Count(z => z == '*');

                            if (liveNeighbors < 2 || liveNeighbors > 3) alive = false;
                            else if (liveNeighbors == 3) alive = true;

                            buff[(x >= w || x < 0) ? x % w : x, (y >= h || y < 0) ? y % h : y] = alive ? '*' : ' ';
                        }
                    }
                    for (var y = 0; y < h; y++)
                    {
                        for (var x = 0; x < w; x++)
                        {
                            console.Write(x, y, buff[x, y]);
                        }
                    }
                    console.Draw();
                    sw.Stop();
                    if (sw.ElapsedMilliseconds < 100)
                    {
                        Thread.Sleep(100 - (int)sw.ElapsedMilliseconds);
                    }
                }
            }
        }

    }
}
