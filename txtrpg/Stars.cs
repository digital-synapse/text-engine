using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamedemo
{
    public class Stars
    {
        public Stars()
        {            
            var rnd = new Random();

            stars = new Point[50];
            for (var i = 0; i < stars.Length; i++)
            {
                var star = new Point();

                star.X = rnd.Next(0, Engine.Console.Width - 1);
                star.Y = rnd.Next(0, Engine.Console.Height - 1);
                stars[i] = star;
            }
        }

        private Point[] stars;

        public void Render()
        {
            foreach (var star in stars)
            {
                star.Y++;
                Engine.Console.Write(star.X, star.Y, '.', true, ConsoleColor.Gray);
            }
        }
    }
}
