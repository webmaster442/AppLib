using System;
using System.Runtime.InteropServices;

namespace Webmaster442.Applib.PInvoke
{
    internal static class Kernel32
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
}
