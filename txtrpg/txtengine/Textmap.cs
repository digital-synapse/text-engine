using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamedemo
{
    public class Textmap : Rect
    {
        public void Init(string textData)
        {
            Textmap.textData = textData;
            // count the lines and width to calculate the size of the rectangle
            var y = 0;
            var w = 0;
            for (var i = 0; i < textData.Length; i++)
            {
                if (textData[i] == '\n')
                {
                    y++;
                    w = i + 1;
                    i += 1;
                }
            }
             
            base.Init(w, y + 1);
        }

        private static string textData; 
        public Rect Rect { get; set; }

        public void Render()
        {
            Engine.Console.Write(Rect.Left, Rect.Top, textData);
        }
    }
}
