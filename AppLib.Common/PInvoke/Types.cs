using System;

namespace AppLib.Common.PInvoke
{
    /// <summary>
    /// Flag values for SetWindowPos
    /// </summary>
    [Flags]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        AsynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        /// contents of the client area are saved and copied back into the client area after the window is sized or
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        /// window uncovered as a result of the window being moved. When this flag is set, the application must
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }

    /// <summary>
    /// An application-defined callback function used with the SendMessageCallback function. 
    /// The system passes the message to the callback function after passing the message to the destination window procedure.
    /// The SENDASYNCPROC type defines a pointer to this callback function.
    /// SendAsyncProc is a placeholder for the application-defined function name.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window whose window procedure received the message. 
    /// If the SendMessageCallback function was called with its hwnd parameter set to HWND_BROADCAST,
    /// the system calls the SendAsyncProc function once for each top-level window.
    /// </param>
    /// <param name="uMsg">The message.</param>
    /// <param name="dwData">An application-defined value sent from the SendMessageCallback function.</param>
    /// <param name="lResult">The result of the message processing. This value depends on the message.</param>
    public delegate void SendMessageDelegate(IntPtr hWnd, uint uMsg, UIntPtr dwData, IntPtr lResult);

    /// <summary>
    /// Flag values for the SendMessageTimeout function
    /// </summary>
    [Flags]
    public enum SendMessageTimeoutFlags : uint
    {
        /// <summary>
        /// The calling thread is not prevented from processing other requests while waiting for the function to return.
        /// </summary>
        SMTO_NORMAL = 0x0,
        /// <summary>
        /// Prevents the calling thread from processing any other requests until the function returns.
        /// </summary>
        SMTO_BLOCK = 0x1,
        /// <summary>
        /// The function returns without waiting for the time-out period to elapse if the receiving thread appears to not respond or "hangs."
        /// </summary>
        SMTO_ABORTIFHUNG = 0x2,
        /// <summary>
        /// The function does not enforce the time-out period as long as the receiving thread is processing messages.
        /// </summary>
        SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
        /// <summary>
        /// The function should return 0 if the receiving window is destroyed or its owning thread dies while the message is being processed.
        /// </summary>
        SMTO_ERRORONEXIT = 0x20
    }

    /// <summary>
    /// Flags for ExitWindowsEx
    /// </summary>
    [Flags]
    public enum ExitWindows : uint
    {
        /// <summary>
        /// Beginning with Windows 8:  You can prepare the system for a faster startup by combining the EWX_HYBRID_SHUTDOWN flag 
        /// with the EWX_SHUTDOWN flag. 
        /// </summary>
        EWX_HYBRID_SHUTDOWN = 0x00400000,
        /// <summary>
        /// Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. 
        /// Then it logs the user off. This flag can be used only by processes running in an interactive user's logon session.
        /// </summary>
        EWX_LOGOFF = 0,
        /// <summary>
        /// Shuts down the system and turns off the power. The system must support the power-off feature. The calling process must have the SE_SHUTDOWN_NAME privilege.
        /// </summary>
        EWX_POWEROFF = 0x00000008,
        /// <summary>
        /// Shuts down the system and then restarts the system. The calling process must have the SE_SHUTDOWN_NAME privilege. 
        /// </summary>
        EWX_REBOOT = 0x00000002,
        /// <summary>
        /// Shuts down the system and then restarts it, as well as any applications that have been registered for restart using the RegisterApplicationRestart function.
        /// These application receive the WM_QUERYENDSESSION message with lParam set to the ENDSESSION_CLOSEAPP value. 
        /// For more information, see Guidelines for Applications.
        /// </summary>
        EWX_RESTARTAPPS = 0x00000040,
        /// <summary>
        /// Shuts down the system to a point at which it is safe to turn off the power. 
        /// All file buffers have been flushed to disk, and all running processes have stopped. 
        /// The calling process must have the SE_SHUTDOWN_NAME privilege.
        /// Specifying this flag will not turn off the power even if the system supports the power-off feature. You must specify EWX_POWEROFF to do this.
        /// Windows XP with SP1:  If the system supports the power-off feature, specifying this flag turns off the power.
        /// </summary>
        EWX_SHUTDOWN = 0x00000001,
        /// <summary>
        /// This flag has no effect if terminal services is enabled. Otherwise, the system does not send the WM_QUERYENDSESSION message.
        /// This can cause applications to lose data. Therefore, you should only use this flag in an emergency.
        /// </summary>
        EWX_FORCE = 0x00000004,
        /// <summary>
        /// Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval.
        /// </summary>
        EWX_FORCEIFHUNG = 0x00000010
    }

    /// <summary>
    /// System shutdown reasons
    /// </summary>
    [Flags]
    public enum ShutdownReason : uint
    {
        /// <summary>
        /// Application issue.
        /// </summary>
        MajorApplication = 0x00040000,
        /// <summary>
        /// Hardware issue.
        /// </summary>
        MajorHardware = 0x00010000,
        /// <summary>
        /// The InitiateSystemShutdown function was used instead of InitiateSystemShutdownEx.
        /// </summary>
        MajorLegacyApi = 0x00070000,
        /// <summary>
        /// Operating system issue.
        /// </summary>
        MajorOperatingSystem = 0x00020000,
        /// <summary>
        /// Other issue.
        /// </summary>
        MajorOther = 0x00000000,
        /// <summary>
        /// Power failure.
        /// </summary>
        MajorPower = 0x00060000,
        /// <summary>
        /// Software issue.
        /// </summary>
        MajorSoftware = 0x00030000,
        /// <summary>
        /// System failure.
        /// </summary>
        MajorSystem = 0x00050000,
        /// <summary>
        /// Blue screen crash event.
        /// </summary>
        MinorBlueScreen = 0x0000000F,
        /// <summary>
        /// Unplugged.
        /// </summary>
        MinorCordUnplugged = 0x0000000b,
        /// <summary>
        /// Disk
        /// </summary>
        MinorDisk = 0x00000007,
        /// <summary>
        /// Environment.
        /// </summary>
        MinorEnvironment = 0x0000000c,
        /// <summary>
        /// Driver.
        /// </summary>
        MinorHardwareDriver = 0x0000000d,
        /// <summary>
        /// Hot fix.
        /// </summary>
        MinorHotfix = 0x00000011,
        /// <summary>
        /// Unresponsive.
        /// </summary>
        MinorHung = 0x00000005,
        /// <summary>
        /// Installation.
        /// </summary>
        MinorInstallation = 0x00000002,
        /// <summary>
        /// Maintenance.
        /// </summary>
        MinorMaintenance = 0x00000001,
        /// <summary>
        /// MMC issue.
        /// </summary>
        MinorMMC = 0x00000019,
        /// <summary>
        /// Network connectivity.
        /// </summary>
        MinorNetworkConnectivity = 0x00000014,
        /// <summary>
        /// Network card.
        /// </summary>
        MinorNetworkCard = 0x00000009,
        /// <summary>
        /// Other issue.
        /// </summary>
        MinorOther = 0x00000000,
        /// <summary>
        /// Other driver event.
        /// </summary>
        MinorOtherDriver = 0x0000000e,
        /// <summary>
        /// Power supply.
        /// </summary>
        MinorPowerSupply = 0x0000000a,
        /// <summary>
        /// Processor.
        /// </summary>
        MinorProcessor = 0x00000008,
        /// <summary>
        /// Reconfigure.
        /// </summary>
        MinorReconfig = 0x00000004,
        /// <summary>
        /// Security issue.
        /// </summary>
        MinorSecurity = 0x00000013,
        /// <summary>
        /// Security patch.
        /// </summary>
        MinorSecurityFix = 0x00000012,
        /// <summary>
        /// Security patch uninstallation.
        /// </summary>
        MinorSecurityFixUninstall = 0x00000018,
        /// <summary>
        /// Service pack.
        /// </summary>
        MinorServicePack = 0x00000010,
        /// <summary>
        /// Service pack uninstallation.
        /// </summary>
        MinorServicePackUninstall = 0x00000016,
        /// <summary>
        /// Terminal Services.
        /// </summary>
        MinorTermSrv = 0x00000020,
        /// <summary>
        /// Unstable.
        /// </summary>
        MinorUnstable = 0x00000006,
        /// <summary>
        /// Upgrade.
        /// </summary>
        MinorUpgrade = 0x00000003,
        /// <summary>
        /// WMI issue.
        /// </summary>
        MinorWMI = 0x00000015,
        /// <summary>
        /// The reason code is defined by the user. For more information, see Defining a Custom Reason Code. If this flag is not present, the reason code is defined by the system.
        /// </summary>
        FlagUserDefined = 0x40000000,
        /// <summary>
        /// The shutdown was planned. The system generates a System State Data (SSD) file. This file contains system state information such as the processes, threads, memory usage, and configuration. 
        /// </summary>
        FlagPlanned = 0x80000000
    }
}
