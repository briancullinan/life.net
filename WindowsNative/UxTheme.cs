using System.Runtime.InteropServices;

namespace WindowsNative
{
    public static class UxTheme
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Margin
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
    }
}
