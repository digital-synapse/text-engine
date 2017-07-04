using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace gamedemo
{
    public class Asteroid : Textmap
    {
        static Asteroid()
        {
            asteroids = XDocument.Load(@"data.xml").Descendants("asteroid").Descendants("frame")
                .Select(element => element.Value).ToArray();

        }
        private static string[] asteroids;
        public Asteroid(int index)
        {
            if (index < 0 || index >= asteroids.Length) throw new ArgumentException(nameof(index));
            this.index = index;
            rnd = new Random();
            base.Init(asteroids[index]);
        }

        private int index;
        private Random rnd;
        private int exploding=0;
        public void Render()
        {
            var a = asteroids[index];
            if (Explode)
            {
                if (exploding < 20)
                {
                    var y = 0;
                    for (var i = 0; i < a.Length; i++)
                    {
                        if (a[i] == '\n')
                        {
                            y++;
                            i++;
                        }
                        else
                        {
                            Engine.Console.Write(Left+(i%y) + rnd.Next(-exploding, exploding), Top+y + rnd.Next(-exploding, exploding), a[i], false, ConsoleColor.White);
                        }
                    }
                    exploding++;
                }
            }
            else
                Engine.Console.Write(Left, Top, asteroids[index], false, ConsoleColor.Gray);
        }      
        
        public bool Explode { get; set; }  
    }

    public class Asteroids
    {
        public Asteroids(Bullets bullets)
        {
            rnd = new Random();
            this.bullets = bullets;
        }
        private Bullets bullets;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private Random rnd;

        public void AddAsteroid()
        {
            var a = new Asteroid(rnd.Next(3));
            a.X = rnd.Next(Engine.Console.Width);
            a.Top = -10;
            asteroids.Add(a);
        }

        public void Render(int frame)
        {
            if (frame % 5 == 0)
            {
                for (var i=0; i<asteroids.Count; i++)
                {
                    var a = asteroids[i];
                    a.Top++;
                    if (a.Top >= Engine.Console.Height)
                    {
                        asteroids.RemoveAt(i);
                    }
                }
            }
            foreach (var a in asteroids) {
                var c=bullets.CollideWith(a);
                if (c > 0)
                {
                    a.Explode = true;
                }
            }

            foreach (var a in asteroids) a.Render();
        }
    }

}
