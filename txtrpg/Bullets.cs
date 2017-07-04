using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamedemo
{
    public class Bullets
    {
        private List<Point> bulletsUP = new List<Point>();
        private List<Point> bulletsDOWN = new List<Point>();

        public void Emit(int originX, int originY, bool up=true)
        {
            if (up)
                bulletsUP.Add(new Point() { X = originX, Y = originY });
            else
                bulletsDOWN.Add(new Point() { X = originX, Y = originY });
        }

        public void Render()
        {
            for (var i = 0; i < bulletsUP.Count; i++)
            {
                var bullet = bulletsUP[i];
                bullet.Y--;
                if (bullet.Y < 0)
                    bulletsUP.Remove(bullet);
                else
                    Engine.Console.Write(bullet.X, bullet.Y, '|', true, ConsoleColor.Yellow);
            }

            for (var i = 0; i < bulletsDOWN.Count; i++)
            {
                var bullet = bulletsDOWN[i];
                bullet.Y++;
                if (bullet.Y < 0)
                    bulletsDOWN.Remove(bullet);
                else
                    Engine.Console.Write(bullet.X, bullet.Y, '|', true, ConsoleColor.Yellow);
            }
        }

        public int CollideWith(Rect r)
        {
            var c= bulletsUP.Count( p => p.X >= r.Left && p.X <= r.Right && p.Y >= r.Top && p.Y <= r.Bottom);
            return c;
        }
    }
}
