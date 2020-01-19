using System;
using System.Runtime.InteropServices;

namespace WindowsNative
{
    public static class WinDef
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public override string ToString()
            {
                return string.Format("Left = {0}, Top = {1}, Right = {2}, Bottom ={3}",
                    Left, Top, Right, Bottom);
            }
            public int Width
            {
                get { return Math.Abs(Right - Left); }
            }
            public int Height
            {
                get { return Math.Abs(Bottom - Top); }
            }
        }
    }
}
