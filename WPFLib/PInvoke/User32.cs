using System;
using System.Runtime.InteropServices;

namespace WPFLib.PInvoke
{
    /// <summary>
    /// Platform Invokes to User32.dll
    /// </summary>
    public static class User32
    {
        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered
        ///     according to their appearance on the screen. The topmost window receives the highest rank and is the first window
        ///     in the Z order.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br />A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        ///     C++ ( hWndInsertAfter [in, optional]. Type: HWND )<br />A handle to the window to precede the positioned window in
        ///     the Z order. This parameter must be a window handle or one of the following values.
        ///     <list type="table">
        ///         <itemheader>
        ///             <term>HWND placement</term><description>Window to precede placement</description>
        ///         </itemheader>
        ///         <item>
        ///             <term>HWND_BOTTOM ((HWND)1)</term>
        ///             <description>
        ///                 Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost
        ///                 window, the window loses its topmost status and is placed at the bottom of all other windows.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>HWND_NOTOPMOST ((HWND)-2)</term>
        ///             <description>
        ///                 Places the window above all non-topmost windows (that is, behind all topmost windows). This
        ///                 flag has no effect if the window is already a non-topmost window.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>HWND_TOP ((HWND)0)</term><description>Places the window at the top of the Z order.</description>
        ///         </item>
        ///         <item>
        ///             <term>HWND_TOPMOST ((HWND)-1)</term>
        ///             <description>
        ///                 Places the window above all non-topmost windows. The window maintains its topmost position
        ///                 even when it is deactivated.
        ///             </description>
        ///         </item>
        ///     </list>
        ///     <para>For more information about how this parameter is used, see the following Remarks section.</para>
        /// </param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />The new position of the left side of the window, in client coordinates.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br />The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">C++ ( cx [in]. Type: int )<br />The new width of the window, in pixels.</param>
        /// <param name="cy">C++ ( cy [in]. Type: int )<br />The new height of the window, in pixels.</param>
        /// <param name="uFlags">
        ///     C++ ( uFlags [in]. Type: UINT )<br />The window sizing and positioning flags. This parameter can be a combination
        ///     of the following values.
        ///     <list type="table">
        ///         <itemheader>
        ///             <term>HWND sizing and positioning flags</term>
        ///             <description>Where to place and size window. Can be a combination of any</description>
        ///         </itemheader>
        ///         <item>
        ///             <term>SWP_ASYNCWINDOWPOS (0x4000)</term>
        ///             <description>
        ///                 If the calling thread and the thread that owns the window are attached to different input
        ///                 queues, the system posts the request to the thread that owns the window. This prevents the calling
        ///                 thread from blocking its execution while other threads process the request.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_DEFERERASE (0x2000)</term>
        ///             <description>Prevents generation of the WM_SYNCPAINT message. </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_DRAWFRAME (0x0020)</term>
        ///             <description>Draws a frame (defined in the window's class description) around the window.</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_FRAMECHANGED (0x0020)</term>
        ///             <description>
        ///                 Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message
        ///                 to the window, even if the window's size is not being changed. If this flag is not specified,
        ///                 WM_NCCALCSIZE is sent only when the window's size is being changed
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_HIDEWINDOW (0x0080)</term><description>Hides the window.</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOACTIVATE (0x0010)</term>
        ///             <description>
        ///                 Does not activate the window. If this flag is not set, the window is activated and moved to
        ///                 the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        ///                 parameter).
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOCOPYBITS (0x0100)</term>
        ///             <description>
        ///                 Discards the entire contents of the client area. If this flag is not specified, the valid
        ///                 contents of the client area are saved and copied back into the client area after the window is sized or
        ///                 repositioned.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOMOVE (0x0002)</term>
        ///             <description>Retains the current position (ignores X and Y parameters).</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOOWNERZORDER (0x0200)</term>
        ///             <description>Does not change the owner window's position in the Z order.</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOREDRAW (0x0008)</term>
        ///             <description>
        ///                 Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies
        ///                 to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///                 parent window uncovered as a result of the window being moved. When this flag is set, the application
        ///                 must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOREPOSITION (0x0200)</term><description>Same as the SWP_NOOWNERZORDER flag.</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOSENDCHANGING (0x0400)</term>
        ///             <description>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOSIZE (0x0001)</term>
        ///             <description>Retains the current size (ignores the cx and cy parameters).</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_NOZORDER (0x0004)</term>
        ///             <description>Retains the current Z order (ignores the hWndInsertAfter parameter).</description>
        ///         </item>
        ///         <item>
        ///             <term>SWP_SHOWWINDOW (0x0040)</term><description>Displays the window.</description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns><c>true</c> or nonzero if the function succeeds, <c>false</c> or zero otherwise or if function fails.</returns>
        /// <remarks>
        ///     <para>
        ///         As part of the Vista re-architecture, all services were moved off the interactive desktop into Session 0.
        ///         hwnd and window manager operations are only effective inside a session and cross-session attempts to manipulate
        ///         the hwnd will fail. For more information, see The Windows Vista Developer Story: Application Compatibility
        ///         Cookbook.
        ///     </para>
        ///     <para>
        ///         If you have changed certain window data using SetWindowLong, you must call SetWindowPos for the changes to
        ///         take effect. Use the following combination for uFlags:
        ///         SWP_NOMOVE
        ///         SWP_NOSIZE
        ///         SWP_NOZORDER 
        ///         SWP_FRAMECHANGED.
        ///     </para>
        ///     <para>
        ///         A window can be made a topmost window either by setting the hWndInsertAfter parameter to HWND_TOPMOST and
        ///         ensuring that the SWP_NOZORDER flag is not set, or by setting a window's position in the Z order so that it is
        ///         above any existing topmost windows. When a non-topmost window is made topmost, its owned windows are also made
        ///         topmost. Its owners, however, are not changed.
        ///     </para>
        ///     <para>
        ///         If neither the SWP_NOACTIVATE nor SWP_NOZORDER flag is specified (that is, when the application requests that
        ///         a window be simultaneously activated and its position in the Z order changed), the value specified in
        ///         hWndInsertAfter is used only in the following circumstances.
        ///     </para>
        ///     <list type="bullet">
        ///         <item>Neither the HWND_TOPMOST nor HWND_NOTOPMOST flag is specified in hWndInsertAfter. </item>
        ///         <item>The window identified by hWnd is not the active window. </item>
        ///     </list>
        ///     <para>
        ///         An application cannot activate an inactive window without also bringing it to the top of the Z order.
        ///         Applications can change an activated window's position in the Z order without restrictions, or it can activate
        ///         a window and then move it to the top of the topmost or non-topmost windows.
        ///     </para>
        ///     <para>
        ///         If a topmost window is repositioned to the bottom (HWND_BOTTOM) of the Z order or after any non-topmost
        ///         window, it is no longer topmost. When a topmost window is made non-topmost, its owners and its owned windows
        ///         are also made non-topmost windows.
        ///     </para>
        ///     <para>
        ///         A non-topmost window can own a topmost window, but the reverse cannot occur. Any window (for example, a
        ///         dialog box) owned by a topmost window is itself made a topmost window, to ensure that all owned windows stay
        ///         above their owner.
        ///     </para>
        ///     <para>
        ///         If an application is not in the foreground, and should be in the foreground, it must call the
        ///         SetForegroundWindow function.
        ///     </para>
        ///     <para>
        ///         To use SetWindowPos to bring a window to the top, the process that owns the window must have
        ///         SetForegroundWindow permission.
        ///     </para>
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        /// <summary>
        /// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST
        /// ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, 
        /// overlapped windows, and pop-up windows; but the message is not sent to child windows. Message sending is subject to UIPI.
        /// The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.
        /// </param>
        /// <param name="Msg">The message to be sent. For lists of the system-provided messages, see System-Defined Messages.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows. 
        /// It calls the window procedure for the specified window and returns immediately if the window belongs to another thread. 
        /// After the window procedure processes the message, the system calls the specified callback function, 
        /// passing the result of the message processing and an application-defined value to the callback function.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. 
        /// If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system,
        /// including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent
        /// to child windows.
        /// </param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <param name="lpCallBack">llback function that the system calls after the window procedure processes the message.
        /// If hWnd is HWND_BROADCAST ((HWND)0xffff), the system calls the SendAsyncProc callback function once for each top-level window.
        /// </param>
        /// <param name="dwData">
        /// An application-defined value to be sent to the callback function pointed to by the lpCallBack parameter.
        /// </param>
        /// <returns>If the function succeeds, the return value is true, otherwise false</returns>
        [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
        static extern bool SendMessageCallback(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageDelegate lpCallBack, UIntPtr dwData);

        /// <summary>
        /// Sends the specified message to one or more windows.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose window procedure will receive the message. 
        /// If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system,
        /// including disabled or invisible unowned windows. The function does not return until each window has timed out. Therefore, 
        /// the total wait time can be up to the value of uTimeout multiplied by the number of top-level windows.
        /// </param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Any additional message-specific information.</param>
        /// <param name="lParam">Any additional message-specific information.</param>
        /// <param name="fuFlags">The behavior of this function</param>
        /// <param name="uTimeout">The duration of the time-out period, in milliseconds. If the message is a broadcast message,
        /// each window can use the full time-out period. 
        /// For example, if you specify a five second time-out period and there are three top-level windows that fail to process the message,
        /// you could have up to a 15 second delay.</param>
        /// <param name="lpdwResult">The result of the message processing. The value of this parameter depends on the message that is specified.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// SendMessageTimeout does not provide information about individual windows timing out if HWND_BROADCAST is used.
        /// If the function fails or times out, the return value is 0. To get extended error information, call GetLastError.
        /// If GetLastError returns ERROR_TIMEOUT, then the function timed out.
        /// </returns>
        [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        /// <summary>
        /// Logs off the interactive user, shuts down the system, or shuts down and restarts the system. It sends the WM_QUERYENDSESSION message to all applications to determine if they can be terminated.
        /// </summary>
        /// <param name="uFlags">The shutdown type</param>
        /// <param name="dwReason">The reason for initiating the shutdown. 
        /// This parameter must be one of the system shutdown reason codes.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is true. Because the function executes asynchronously, 
        /// a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed.
        /// It is possible that the system, the user, or another application will abort the shutdown. 
        /// If the function fails, the return value is false. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);

    }
}
