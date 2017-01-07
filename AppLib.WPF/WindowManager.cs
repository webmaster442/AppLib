using AppLib.Common.PInvoke;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace AppLib.WPF
{
    /// <summary>
    /// Provides functions for general Window management tasks
    /// </summary>
    public static class WindowManagement
    {
        private static List<WindowInformation> _windows;
        private static IntPtr _caller;
        private static IntPtr _shell;
        private readonly static uint _IsVisible;

        static WindowManagement()
        {
            _windows = new List<WindowInformation>();
            _caller = IntPtr.Zero;
            _shell = User32.GetShellWindow();
            _IsVisible = GWLFlags.WS_VISIBLE | GWLFlags.WS_EX_APPWINDOW;
        }

        private static bool IsWindowChainVisible(IntPtr hWnd)
        {
            // Start at the root owner
            IntPtr hwndWalk = User32.GetAncestor(hWnd, GetAncestorFlags.GetRootOwner);
            // Basically we try get from the parent back to that window
            IntPtr hwndTry;
            while ((hwndTry = User32.GetLastActivePopup(hwndWalk)) != hwndTry)
            {
                if (User32.IsWindowVisible(hwndTry)) break;
                hwndWalk = hwndTry;
            }
            return (hwndWalk == hWnd);
        }

        private static bool enumWindowsCall(IntPtr hwnd, int lParam)
        {
            var visible = (User32.GetWindowLong(hwnd, GWLFlags.GWL_STYLE) & _IsVisible) == _IsVisible;
            var isshell = (hwnd == _shell);
            if (visible && !isshell && IsWindowChainVisible(hwnd))
            {
                var sb = new StringBuilder();
                sb.Capacity = User32.GetWindowTextLength(hwnd) + 1;
                User32.GetWindowText(hwnd, sb, sb.Capacity);
                if (sb.Length > 0)
                {
                    var wi = new WindowInformation(hwnd, sb.ToString());
                    if ((_caller != IntPtr.Zero) && (_caller != hwnd))
                        _windows.Add(wi);
                    else
                        _windows.Add(wi);
                }
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
        /// Provides a list of Window Informations
        /// </summary>
        /// <param name="caller">Caller window</param>
        /// <returns></returns>
        public static IList<WindowInformation> GetWindowList(Window caller)
        {
            var interop = new WindowInteropHelper(caller).Handle;
            return GetWindowList(interop);
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
