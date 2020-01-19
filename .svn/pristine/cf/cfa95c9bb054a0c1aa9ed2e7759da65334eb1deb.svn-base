using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsNative
{
    public static class Shell32
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        internal static extern int SHGetStockIconInfo(ShellApi.StockIconIdentifier identifier,
                                                      ShellApi.StockIconOptions flags, ref ShellApi.StockIconInfo info);
    }
}
