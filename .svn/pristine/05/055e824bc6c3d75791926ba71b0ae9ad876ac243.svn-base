using System;
using System.Runtime.InteropServices;
using Winumeration.Api;

namespace WindowsNative
{
    public static class DwmApi
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref UxTheme.Margin pMarInset);
    }
}
