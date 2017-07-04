using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamedemo
{
    public class Enemy : Textmap
    {
        private static string[] enemy = new[]
            { "/[ ]\\" + Environment.NewLine +
              "  V  "};

        public Enemy()
        {
            base.Init(enemy[0]);
        }

    }
}
