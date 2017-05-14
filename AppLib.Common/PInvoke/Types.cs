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
        /// <summary>
        /// Retrieves a handle to the parent window, if any.
        /// </summary>
        public const int GWL_HWNDPARENT = (-8);
        /// <summary>
        /// Retrieves the identifier of the window.
        /// </summary>
        public const int GWL_ID = (-12);
        /// <summary>
        /// Retrieves the window styles.
        /// </summary>
        public const int GWL_STYLE = (-16);
        /// <summary>
        /// Retrieves the extended window styles.
        /// </summary>
        public const int GWL_EXSTYLE = (-20);
        // Window Styles
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
        /// </summary>
        public const uint WS_OVERLAPPED = 0;
        /// <summary>
        /// The windows is a pop-up window. This style cannot be used with the WS_CHILD style.
        /// </summary>
        public const uint WS_POPUP = 0x80000000;
        /// <summary>
        /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
        /// </summary>
        public const uint WS_CHILD = 0x40000000;
        /// <summary>
        /// The window is initially minimized. Same as the WS_ICONIC style.
        /// </summary>
        public const uint WS_MINIMIZE = 0x20000000;
        /// <summary>
        /// The window is initially visible.
        /// </summary>
        public const uint WS_VISIBLE = 0x10000000;
        /// <summary>
        /// The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.
        /// </summary>
        public const uint WS_DISABLED = 0x8000000;
        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
        /// </summary>
        public const uint WS_CLIPSIBLINGS = 0x4000000;
        /// <summary>
        /// Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
        /// </summary>
        public const uint WS_CLIPCHILDREN = 0x2000000;
        /// <summary>
        /// The window is initially maximized.
        /// </summary>
        public const uint WS_MAXIMIZE = 0x1000000;
        /// <summary>
        /// The window has a title bar (includes the WS_BORDER style).
        /// </summary>
        public const uint WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME
        /// <summary>
        /// The window has a thin-line border.
        /// </summary>
        public const uint WS_BORDER = 0x800000;
        /// <summary>
        /// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
        /// </summary>
        public const uint WS_DLGFRAME = 0x400000;
        /// <summary>
        /// The window has a vertical scroll bar.
        /// </summary>
        public const uint WS_VSCROLL = 0x200000;
        /// <summary>
        /// The window has a horizontal scroll bar.
        /// </summary>
        public const uint WS_HSCROLL = 0x100000;
        /// <summary>
        /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
        /// </summary>
        public const uint WS_SYSMENU = 0x80000;
        /// <summary>
        /// The window has a sizing border. Same as the WS_SIZEBOX style.
        /// </summary>
        public const uint WS_THICKFRAME = 0x40000;
        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        /// </summary>
        public const uint WS_GROUP = 0x20000;
        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        /// </summary>
        public const uint WS_TABSTOP = 0x10000;
        /// <summary>
        /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. 
        /// </summary>
        public const uint WS_MINIMIZEBOX = 0x20000;
        /// <summary>
        /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. 
        /// </summary>
        public const uint WS_MAXIMIZEBOX = 0x10000;
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style. 
        /// </summary>
        public const uint WS_TILED = WS_OVERLAPPED;
        /// <summary>
        /// The window is initially minimized. Same as the WS_MINIMIZE style.
        /// </summary>
        public const uint WS_ICONIC = WS_MINIMIZE;
        /// <summary>
        /// The window has a sizing border. Same as the WS_THICKFRAME style.
        /// </summary>
        public const uint WS_SIZEBOX = WS_THICKFRAME;
        // Extended Window Styles
        /// <summary>
        /// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
        /// </summary>
        public const uint WS_EX_DLGMODALFRAME = 0x0001;
        /// <summary>
        /// The child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        /// </summary>
        public const uint WS_EX_NOPARENTNOTIFY = 0x0004;
        /// <summary>
        /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
        /// </summary>
        public const uint WS_EX_TOPMOST = 0x0008;
        /// <summary>
        /// The window accepts drag-drop files.
        /// </summary>
        public const uint WS_EX_ACCEPTFILES = 0x0010;
        /// <summary>
        /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// </summary>
        public const uint WS_EX_TRANSPARENT = 0x0020;
        /// <summary>
        /// The window is a MDI child window.
        /// </summary>
        public const uint WS_EX_MDICHILD = 0x0040;
        /// <summary>
        /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
        /// </summary>
        public const uint WS_EX_TOOLWINDOW = 0x0080;
        /// <summary>
        /// The window has a border with a raised edge.
        /// </summary>
        public const uint WS_EX_WINDOWEDGE = 0x0100;
        /// <summary>
        /// The window has a border with a sunken edge.
        /// </summary>
        public const uint WS_EX_CLIENTEDGE = 0x0200;
        /// <summary>
        /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
        /// </summary>
        public const uint WS_EX_CONTEXTHELP = 0x0400;
        /// <summary>
        /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
        /// </summary>
        public const uint WS_EX_RIGHT = 0x1000;
        /// <summary>
        /// The window has generic left-aligned properties. This is the default.
        /// </summary>
        public const uint WS_EX_LEFT = 0x0000;
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
        /// </summary>
        public const uint WS_EX_RTLREADING = 0x2000;
        /// <summary>
        /// The window text is displayed using left-to-right reading-order properties. This is the default.
        /// </summary>
        public const uint WS_EX_LTRREADING = 0x0000;
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
        /// </summary>
        public const uint WS_EX_LEFTSCROLLBAR = 0x4000;
        /// <summary>
        /// The vertical scroll bar (if present) is to the right of the client area. This is the default.
        /// </summary>
        public const uint WS_EX_RIGHTSCROLLBAR = 0x0000;
        /// <summary>
        /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        public const uint WS_EX_CONTROLPARENT = 0x10000;
        /// <summary>
        /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        public const uint WS_EX_STATICEDGE = 0x20000;
        /// <summary>
        /// Forces a top-level window onto the taskbar when the window is visible. 
        /// </summary>
        public const uint WS_EX_APPWINDOW = 0x40000;
        /// <summary>
        /// The window is an overlapped window.
        /// </summary>
        public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        /// <summary>
        /// The window is palette window, which is a modeless dialog box that presents an array of commands. 
        /// </summary>
        public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        /// <summary>
        ///  The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        ///  Windows 8:  The WS_EX_LAYERED style is supported for top-level windows and child windows.Previous Windows versions support WS_EX_LAYERED only for top-level windows.
        /// </summary>
        public const uint WS_EX_LAYERED = 0x00080000;
        /// <summary>
        /// The window does not pass its window layout to its child windows.
        /// </summary>
        public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left. 
        /// </summary>
        public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        /// <summary>
        /// Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
        /// </summary>
        public const uint WS_EX_COMPOSITED = 0x02000000;
        /// <summary>
        ///  A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        ///  To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        ///  The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        /// </summary>
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

    /// <summary>
    /// Console configuration flags
    /// </summary>
    public static class ConsoleModes
    {
        /// <summary>
        /// Setting this flag directs the Virtual Terminal processing engine to convert user input received by the console window into Console Virtual Terminal Sequences that can be retrieved by a supporting application through WriteFile or WriteConsole functions.
        /// </summary>
        public const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        /// <summary>
        /// When writing with WriteFile or WriteConsole, this adds an additional state to end-of-line wrapping that can delay the cursor move and buffer scroll operations. 
        /// </summary>
        public const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
        /// <summary>
        /// Setting this flag directs the Virtual Terminal processing engine to convert user input received by the console window into Console Virtual Terminal Sequences that can be retrieved by a supporting application through WriteFile or WriteConsole functions.
        /// </summary>
        public const uint ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;
        /// <summary>
        /// Characters read by the ReadFile or ReadConsole function are written to the active screen buffer as they are read. This mode can be used only if the ENABLE_LINE_INPUT mode is also enabled.
        /// </summary>
        public const uint ENABLE_ECHO_INPUT = 0x0004;
    }
}
