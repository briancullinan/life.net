using System;
using System.Runtime.InteropServices;
using Winumeration.Api;

namespace WindowsNative
{
    public static class Gdi32
    {
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public extern static IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public extern static IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public extern static IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public extern static bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, WinGdi.TernaryRasterOperations RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public extern static IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public extern static IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop);

        [DllImport("gdi32.dll")]
        public static extern int SetStretchBltMode(IntPtr hdc, int iStretchMode);
    }
}
