using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace gamedemo
{
    public static class Menu
    {
        private static string title;
        static Menu()
        {
            title = XDocument.Load(@"data.xml").Descendants("title")
                .Select(element => element.Value).FirstOrDefault();
        }
        public static void Run(Stars stars)
        {            
            while (!Console.KeyAvailable)
            {
                Engine.Console.Clear();
                stars.Render();
                Engine.Console.Write(20, 10, title);
                Engine.Console.Draw();
            }

        }
    }
}
