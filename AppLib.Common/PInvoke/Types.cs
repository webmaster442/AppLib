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

    /// <summary>
    /// Flags that can be used in LoadLibaryEx
    /// </summary>
    [Flags]
    public enum LoadLibraryFlags : uint
    {
        /// <summary>
        /// If this value is used, and the executable module is a DLL, the system does not call DllMain for process and thread initialization and termination. Also, the system does not load additional executable modules that are referenced by the specified module.
        /// Note: Do not use this value; it is provided only for backward compatibility. If you are planning to access only data or resources in the DLL, use LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or LOAD_LIBRARY_AS_IMAGE_RESOURCE or both. Otherwise, load the library as a DLL or executable module using the LoadLibrary function.
        /// </summary>
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
        /// <summary>
        /// If this value is used, the system does not check AppLocker rules or apply Software Restriction Policies for the DLL. This action applies only to the DLL being loaded and not to its dependencies. This value is recommended for use in setup programs that must run extracted DLLs during installation.
        /// Windows Server 2008 R2 and Windows 7:  On systems with KB2532445 installed, the caller must be running as "LocalSystem" or "TrustedInstaller"; otherwise the system ignores this flag. For more information, see "You can circumvent AppLocker rules by using an Office macro on a computer that is running Windows 7 or Windows Server 2008 R2" in the Help and Support Knowledge Base at http://support.microsoft.com/kb/2532445.
        /// Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP:  AppLocker was introduced in Windows 7 and Windows Server 2008 R2.
        /// </summary>
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
        /// <summary>
        /// If this value is used, the system maps the file into the calling process's virtual address space as if it were a data file. Nothing is done to execute or prepare to execute the mapped file. Therefore, you cannot call functions like GetModuleFileName, GetModuleHandle or GetProcAddress with this DLL. Using this value causes writes to read-only memory to raise an access violation. Use this flag when you want to load a DLL only to extract messages or resources from it.
        /// This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE. For more information, see Remarks.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
        /// <summary>
        /// Similar to LOAD_LIBRARY_AS_DATAFILE, except that the DLL file is opened with exclusive write access for the calling process. Other processes cannot open the DLL file for write access while it is in use. However, the DLL can still be opened by other processes.
        /// This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE. For more information, see Remarks.
        /// Windows Server 2003 and Windows XP:  This value is not supported until Windows Vista.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
        /// <summary>
        /// If this value is used, the system maps the file into the process's virtual address space as an image file. However, the loader does not load the static imports or perform the other usual initialization steps. Use this flag when you want to load a DLL only to extract messages or resources from it.
        /// Unless the application depends on the file having the in-memory layout of an image, this value should be used with either LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or LOAD_LIBRARY_AS_DATAFILE. For more information, see the Remarks section.
        /// Windows Server 2003 and Windows XP:  This value is not supported until Windows Vista.
        /// </summary>
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
        /// <summary>
        /// If this value is used, the application's installation directory is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
        /// <summary>
        /// This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR, LOAD_LIBRARY_SEARCH_SYSTEM32, and LOAD_LIBRARY_SEARCH_USER_DIRS. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// This value represents the recommended maximum number of directories an application should include in its DLL search path.
        /// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
        /// <summary>
        /// If this value is used, the directory that contains the DLL is temporarily added to the beginning of the list of directories that are searched for the DLL's dependencies. Directories in the standard search path are not searched.
        /// The lpFileName parameter must specify a fully qualified path. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// For example, if Lib2.dll is a dependency of C:\Dir1\Lib1.dll, loading Lib1.dll with this value causes the system to search for Lib2.dll only in C:\Dir1. To search for Lib2.dll in C:\Dir1 and all of the directories in the DLL search path, combine this value with LOAD_LIBRARY_DEFAULT_DIRS.
        /// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
        /// <summary>
        /// If this value is used, %windows%\system32 is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
        /// <summary>
        /// If this value is used, directories added using the AddDllDirectory or the SetDllDirectory function are searched for the DLL and its dependencies. If more than one directory has been added, the order in which the directories are searched is unspecified. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires KB2533623 to be installed.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
        /// <summary>
        /// If this value is used and lpFileName specifies an absolute path, the system uses the alternate file search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded. If this value is used and lpFileName specifies a relative path, the behavior is undefined.
        /// If this value is not used, or if lpFileName does not specify a path, the system uses the standard search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded.
        /// This value cannot be combined with any LOAD_LIBRARY_SEARCH flag.
        /// </summary>
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
    }

    /// <summary>
    /// Flags used in CascadeWindows &amp; TileWindows
    /// </summary>
    [Flags]
    public enum wHowFlags : uint
    {
        /// <summary>
        /// Tiles windows horizontally.
        /// </summary>
        MDITILE_HORIZONTAL = 0x0001,
        /// <summary>
        /// Tiles windows vertically.
        /// </summary>
        MDITILE_VERTICAL = 0x0000,
        /// <summary>
        /// Prevents disabled MDI child windows from being cascaded. 
        /// </summary>
        MDITILE_SKIPDISABLED = 0x0002,
        /// <summary>
        /// Arranges the windows in Z order.
        /// </summary>
        MDITILE_ZORDER = 0x0004
    }

    /// <summary>
    /// An application-defined callback function used with the EnumWindows or EnumDesktopWindows function. It receives top-level window handles. The WNDENUMPROC type defines a pointer to this callback function. EnumWindowsProc is a placeholder for the application-defined function name. 
    /// </summary>
    /// <param name="hwnd">A handle to a top-level window.</param>
    /// <param name="lParam">The application-defined value given in EnumWindows or EnumDesktopWindows.</param>
    /// <returns>To continue enumeration, the callback function must return TRUE; to stop enumeration, it must return FALSE. </returns>
    public delegate bool EnumWindowsProc(IntPtr hwnd, int lParam);

    /// <summary>
    /// GetWindowLong flags
    /// </summary>
    public static class GWLFlags
    {
        public const int GWL_HWNDPARENT = (-8);
        public const int GWL_ID = (-12);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        // Window Styles
        public const uint WS_OVERLAPPED = 0;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_DISABLED = 0x8000000;
        public const uint WS_CLIPSIBLINGS = 0x4000000;
        public const uint WS_CLIPCHILDREN = 0x2000000;
        public const uint WS_MAXIMIZE = 0x1000000;
        public const uint WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
        public const uint WS_BORDER = 0x800000;
        public const uint WS_DLGFRAME = 0x400000;
        public const uint WS_VSCROLL = 0x200000;
        public const uint WS_HSCROLL = 0x100000;
        public const uint WS_SYSMENU = 0x80000;
        public const uint WS_THICKFRAME = 0x40000;
        public const uint WS_GROUP = 0x20000;
        public const uint WS_TABSTOP = 0x10000;
        public const uint WS_MINIMIZEBOX = 0x20000;
        public const uint WS_MAXIMIZEBOX = 0x10000;
        public const uint WS_TILED = WS_OVERLAPPED;
        public const uint WS_ICONIC = WS_MINIMIZE;
        public const uint WS_SIZEBOX = WS_THICKFRAME;
        // Extended Window Styles
        public const uint WS_EX_DLGMODALFRAME = 0x0001;
        public const uint WS_EX_NOPARENTNOTIFY = 0x0004;
        public const uint WS_EX_TOPMOST = 0x0008;
        public const uint WS_EX_ACCEPTFILES = 0x0010;
        public const uint WS_EX_TRANSPARENT = 0x0020;
        public const uint WS_EX_MDICHILD = 0x0040;
        public const uint WS_EX_TOOLWINDOW = 0x0080;
        public const uint WS_EX_WINDOWEDGE = 0x0100;
        public const uint WS_EX_CLIENTEDGE = 0x0200;
        public const uint WS_EX_CONTEXTHELP = 0x0400;
        public const uint WS_EX_RIGHT = 0x1000;
        public const uint WS_EX_LEFT = 0x0000;
        public const uint WS_EX_RTLREADING = 0x2000;
        public const uint WS_EX_LTRREADING = 0x0000;
        public const uint WS_EX_LEFTSCROLLBAR = 0x4000;
        public const uint WS_EX_RIGHTSCROLLBAR = 0x0000;
        public const uint WS_EX_CONTROLPARENT = 0x10000;
        public const uint WS_EX_STATICEDGE = 0x20000;
        public const uint WS_EX_APPWINDOW = 0x40000;
        public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        public const uint WS_EX_COMPOSITED = 0x02000000;
        public const uint WS_EX_NOACTIVATE = 0x08000000;
    }

    /// <summary>
    /// Flags used by the GetAncestor API call
    /// </summary>
    public enum GetAncestorFlags
    {
        /// <summary>
        /// Retrieves the parent window. This does not include the owner, as it does with the GetParent function.
        /// </summary>
        GetParent = 1,
        /// <summary>
        /// Retrieves the root window by walking the chain of parent windows.
        /// </summary>
        GetRoot = 2,
        /// <summary>
        /// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
        /// </summary>
        GetRootOwner = 3
    }
}
