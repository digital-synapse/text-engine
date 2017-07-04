using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace txtengine
{
    public class ConsoleEngine : IDisposable
    {
        public ConsoleEngine()
        {           
            InterceptKeys.Run();

            var width = Console.WindowWidth;
            var height = Console.WindowHeight;
            Console.CursorVisible = false;
            Console.SetBufferSize(width, height);
            this.width = (short)width;
            this.height = (short)height;

            Clear();
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        }

        public bool KeyDown(System.ConsoleKey key)
        {
            return InterceptKeys.keys[(int)key];
        }
    

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }


        public short Width => width;
        public short Height => height;

        SafeFileHandle h;
        CharInfo[] buf;

        short width;
        short height;
        SmallRect rect;
        Coord bufferSize;
        Coord bufferOffset;

        public void Clear()
        {
            rect = new SmallRect() { Left = 0, Top = 0, Right = width, Bottom = height };
            bufferSize = new Coord() { X = width, Y = height };
            bufferOffset = new Coord() { X = 0, Y = 0 };
            buf = new CharInfo[width * height];
            //Console.Clear();
        }

        public void Draw()
        {
            if (!h.IsInvalid)
            {
                bool b = WriteConsoleOutput(h, buf, bufferSize, bufferOffset, ref rect);

                if (!b) Debugger.Break();
            }
            else throw new InvalidOperationException();
        }

        private int wrap(int val, int max)
        {
            if (val >= max) return val % max;
            else if (val < 0) return (max*10000 + val)%max;
            else return val;
        }

        public void Write(int x, int y, char c,bool wrapCoordinates=true, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            // wrap
            if (wrapCoordinates)
            {
                x = wrap(x, width);
                y = wrap(y, height);
            }
            else
            {
                if (x >= width || x < 0 || y >= height || y < 0) return;
            }

            var p = y * width + x;
            buf[p].Char.UnicodeChar = c;
            var bg = ((int)background << 4);
            buf[p].Attributes = (short)(((int)foreground) | bg);
        }
        public void Write(int x, int y, string str, bool wrapCoordinates=true, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            var lines= str.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            for (var iy = 0; iy < lines.Length; iy++)
            {
                var line = lines[iy];
                for (var ix = 0; ix < line.Length; ix++)
                {
                    Write(x+ix,y+iy, line[ix], wrapCoordinates, foreground,background);
                }
            }
        }


        public char Read(int x, int y)
        {
            // wrap
            x = wrap(x, width);
            y = wrap(y, height);

            var p = y * width + x;
            return buf[p].Char.UnicodeChar;
        }
        /*
        public void Demo()
        {           

            if (!h.IsInvalid)
            {                
                int i = 0;
                for (byte character = 65; character < 65 + 26; ++character)
                {
                    for (short attribute = 0; attribute < 15; ++attribute)
                    {
                        
                            buf[i].Attributes = attribute;
                            buf[i].Char.AsciiChar = character;
                        i++;

                        
                    }
                }
                bool b = WriteConsoleOutput(h, buf, bufferSize, bufferOffset, ref rect);
            }
            Console.ReadKey();
        }       
        */

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                h.Close();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~ConsoleEngine() {
          // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
          Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}
