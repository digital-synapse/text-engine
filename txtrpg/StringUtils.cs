using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txtrpg
{
    public static class StringUtils
    {
        private static StringBuilder sb = new StringBuilder();

        public static string Repeat(char c, int repeat)
        {
            sb.Clear();
            for (var i = 0; i < repeat; i++) {
                sb.Append(c);
            }
            return sb.ToString();
        }



        private static char[,] generateRandomDungeon(int w, int h, int seed)
        {
            var rng = new Random(seed);
            var pageWidth = Console.WindowWidth;
            var pageHeight = Console.WindowHeight;
            var mapHeight = h * pageHeight;
            var mapWidth = w * pageWidth;
            var dungeon = new char[mapWidth, mapHeight];

            // define some characters
            var wall = '▒';
            var empty = ' ';

            // step 1 fill dungeon with walls
            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    dungeon[x, y] = wall;
                }
            }

            // step 2 generate rooms
            for (var i = 0; i < rng.Next(5, 20); i++)
            {
                var startx = rng.Next(1, 1 + mapWidth - pageWidth);
                var starty = rng.Next(1, 1 + mapHeight - pageHeight);
                var width = rng.Next(5, pageWidth - 1);
                var height = rng.Next(5, pageHeight - 1);

                for (var y = starty; y < starty + height; y++)
                {
                    for (var x = startx; x < startx + width; x++)
                    {
                        dungeon[x, y] = empty;
                    }
                }

            }


            return dungeon;
        }

    }
}
