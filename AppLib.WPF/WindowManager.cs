using AppLib.Common.PInvoke;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.WPF
{
    /// <summary>
    /// Provides functions for general Window management tasks
    /// </summary>
    public static class WindowManagement
    {
        private static List<WindowInformation> _windows;
        private static IntPtr _caller;
        private static IntPtr _thumbnail;
        private readonly static uint _IsVisible;

        static WindowManagement()
        {
            _windows = new List<WindowInformation>();
            _IsVisible = GWLFlags.WS_BORDER | GWLFlags.WS_VISIBLE;
            _caller = IntPtr.Zero;
        }

        private static bool enumWindowsCall(IntPtr hwnd, int lParam)
        {
            if ((User32.GetWindowLong(hwnd, GWLFlags.GWL_STYLE) & _IsVisible) == _IsVisible)
            {
                var sb = new StringBuilder();
                sb.Capacity = User32.GetWindowTextLength(hwnd);
                User32.GetWindowText(hwnd, sb, sb.Capacity);
                var wi = new WindowInformation(hwnd, sb.ToString());
                if ((_caller != IntPtr.Zero) && (_caller != hwnd))
                    _windows.Add(wi);
                else
                    _windows.Add(wi);
            }
            return true;
        }

        /// <summary>
        /// Provides a list of Window Informations
        /// </summary>
        /// <param name="caller">Caller window pointer. If its Zero, then all windows returned, otherwise the caller is skipped</param>
        public static IList<WindowInformation> GetWindowList(IntPtr caller)
        {
            _windows.Clear();
            User32.EnumWindows(enumWindowsCall, 0);
            return _windows;
        }

        /// <summary>
        /// Cascades all windows
        /// </summary>
        public static void CascadeWindows()
        {
            User32.CascadeWindows(IntPtr.Zero, wHowFlags.MDITILE_ZORDER, IntPtr.Zero, 0, null);
        } 

        /// <summary>
        /// Tiles all windows vertically
        /// </summary>
        public static void TileWindowsVertically()
        {
            User32.TileWindows(IntPtr.Zero, wHowFlags.MDITILE_VERTICAL, IntPtr.Zero, 0, null);
        }

        /// <summary>
        /// Tiles all windows horizontally
        /// </summary>
        public static void TileWindowsHorizontally()
        {
            User32.TileWindows(IntPtr.Zero, wHowFlags.MDITILE_HORIZONTAL, IntPtr.Zero, 0, null);
        }
    }

    /// <summary>
    /// Window Information 
    /// </summary>
    public class WindowInformation
    {
        /// <summary>
        /// Window handle
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Window title string
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Creates a new instance of window information
        /// </summary>
        /// <param name="handle">Window handle</param>
        /// <param name="title">Window title</param>
        public WindowInformation(IntPtr handle, string title)
        {
            Handle = handle;
            Title = title;
        }
    }
}
