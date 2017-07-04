using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using txtengine;

namespace gamedemo
{
    public static class Engine
    {
        public static ConsoleEngine Console { get; set; } = new ConsoleEngine();
        public static bool KeyDown(ConsoleKey key) { return Console.KeyDown(key); }
    }

    class Program
    {       
        static void Main(string[] args)
        {
            var console = Engine.Console;
            var w= console.Width;
            var h = console.Height;

            var stars = new Stars();

            Menu.Run(stars);

            var enemy = new Enemy();
            enemy.X = 10;
            enemy.Y = 5;

            
            var bullets = new Bullets();
            var player = new Player(bullets);
            var asteroids = new Asteroids(bullets);

            var frame = 0;
                        
            bool running = true;
            while (running)
            {
                frame++;

                if (frame % 100 == 0)
                {
                    asteroids.AddAsteroid();
                }

                running= player.Logic(frame);
                
                console.Clear();

                bullets.Render();
                stars.Render();
                
                asteroids.Render(frame);

                player.Render();
                console.Draw();
            }
        }
    }
        
}

