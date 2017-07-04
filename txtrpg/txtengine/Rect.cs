using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace gamedemo
{
    public class Rect
    {
        public void Init(int width, int height)
        {
            this.width = this.right = width;
            this.height = this.bottom = height;
            x=halfw = width / 2;
            y=halfh = height / 2;

        }

        public int X { get { return x; }
            set
            {
                x = value;
                left = value - halfw;
                right = left + width;
            }
        }

        public int Y {  get { return y; }
            set
            {
                y = value;
                top = value - halfh;
                bottom = top + height;
            }
        }

        public int Left { get { return left; }
            set
            {
                left = value;
                right = left+width;
                x = left + halfw;
            }
        }
        public int Top { get { return top; }
            set
            {
                top = value;
                bottom = top + height;
                y = top + halfh;
            }
        }
        public int Bottom { get { return bottom; }
            set {
                bottom = value;
                top = bottom-height;
                y = top + halfh;
            }
        }
        public int Right { get { return right; }
            set {
                right = value;
                left = right - width;
                x = left + halfw;
            }
        }
        private int x;
        private int y;
        private int left;
        private int top;
        private int right;
        private int bottom;
        private int width;
        private int height;
        private int halfw;
        private int halfh;
        public int Width => width;
        public int Height => height;
    }
}
