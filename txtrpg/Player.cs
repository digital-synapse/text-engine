using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace gamedemo
{
    public class Player
    {
        static Player()
        {
            ship = XDocument.Load(@"data.xml").Descendants("ship").Descendants("frame")
                .Select(element => element.Value).ToArray();

        }

        public Player( Bullets bullets )
        {
            this.bullets = bullets;            
            shipx = Engine.Console.Width / 2;
            shipy = 0;
            shipframe = 0;
            h = Engine.Console.Height;
        }
        private Bullets bullets;
        private int shipx;
        private double shipy;
        private int shipframe;
        private static string[] ship;
        private int h;

        public bool Logic( int frame )
        {
            shipframe = 0;
            if (Engine.KeyDown(ConsoleKey.Escape))
            {
                return false;
            }
            if (Engine.KeyDown(ConsoleKey.RightArrow))
            {
                shipx++;
                shipframe = 1;
            }
            if (Engine.KeyDown(ConsoleKey.LeftArrow))
            {
                shipx--;
                shipframe = 2;
            }
            if (Engine.KeyDown(ConsoleKey.UpArrow))
            {
                shipy -= .2;
                if (shipy < -15) shipy = -15;
            }
            else
            {
                shipy += .3;
                if (shipy > 0) shipy = 0;
            }
            if (Engine.KeyDown(ConsoleKey.Spacebar))
            {
                if (frame % 8 == 0)
                    bullets.Emit(shipx, ((int)shipy) + h - 5);
            }
            return true;
        }

        public void Render()
        {
            Engine.Console.Write(shipx - 3, ((int)shipy) + h - 5, ship[shipframe]);
        }
    }
}
