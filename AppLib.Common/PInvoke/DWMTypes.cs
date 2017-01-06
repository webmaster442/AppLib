using System;
using System.Runtime.InteropServices;

namespace AppLib.Common.PInvoke
{
    /// <summary>
    /// DWM Colors structiure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWMCOLORIZATIONPARAMS
    {
        /// <summary>
        /// Current theme color
        /// </summary>
        public uint ColorizationColor;
        /// <summary>
        /// After Glow aplied color
        /// </summary>
        public uint ColorizationAfterglow;
        /// <summary>
        /// Color ballance
        /// </summary>
        public uint ColorizationColorBalance;
        /// <summary>
        /// After glow aplied color ballance
        /// </summary>
        public uint ColorizationAfterglowBalance;
        /// <summary>
        /// Blur ballance
        /// </summary>
        public uint ColorizationBlurBalance;
        /// <summary>
        /// Glass Reflection intensity
        /// </summary>
        public uint ColorizationGlassReflectionIntensity;
        /// <summary>
        /// Opaque Blend
        /// </summary>
        public uint ColorizationOpaqueBlend;
    }

    /// <summary>
    /// The SIZE structure specifies the width and height of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        /// <summary>
        /// Specifies the rectangle's width. The units depend on which function uses this.
        /// </summary>
        public long cX;

        /// <summary>
        /// Specifies the rectangle's height. The units depend on which function uses this.
        /// </summary>
        public long cY;
    }

    /// <summary>
    /// Specifies Desktop Window Manager (DWM) thumbnail properties. Used by the DwmUpdateThumbnailProperties function.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_THUMBNAIL_PROPERTIES
    {
        /// <summary>
        /// A bitwise combination of DWM thumbnail constant values that indicates which members of this structure are set.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public dwFlags dwFlags;
        /// <summary>
        /// The area in the destination window where the thumbnail will be rendered.
        /// </summary>
        public Rect rcDestination;
        /// <summary>
        /// The region of the source window to use as the thumbnail. By default, the entire window is used as the thumbnail.
        /// </summary>
        public Rect rcSource;
        /// <summary>
        /// The opacity with which to render the thumbnail. 0 is fully transparent while 255 is fully opaque. The default value is 255.
        /// </summary>
        public byte opacity;
        /// <summary>
        /// TRUE to make the thumbnail visible; otherwise, FALSE. The default is FALSE.
        /// </summary>
        public bool fVisible;
        /// <summary>
        /// TRUE to use only the thumbnail source's client area; otherwise, FALSE. The default is FALSE.
        /// </summary>
        public bool fSourceClientAreaOnly;
    }

    /// <summary>
    /// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        /// <summary>
        /// Creates a new instance of rect
        /// </summary>
        /// <param name="left">The x-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="right">The x-coordinate of the lower-right corner of the rectangle.</param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
        public Rect(long left, long top, long right, long bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public long Left;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public long Top;


        /// <summary>
        /// The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public long Right;

        /// <summary>
        /// The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public long Bottom;
    }


    /// <summary>
    /// Flags used by the DWM_THUMBNAIL_PROPERTIES structure to indicate which of its members contain valid information.
    /// </summary>
    [Flags]
    public enum dwFlags: int
    {
        /// <summary>
        /// A value for the rcDestination member has been specified.
        /// </summary>
        DWM_TNP_RECTDESTINATION = 0x00000001,
        /// <summary>
        /// A value for the rcSource member has been specified.
        /// </summary>
        DWM_TNP_RECTSOURCE = 0x00000002,
        /// <summary>
        /// A value for the opacity member has been specified.
        /// </summary>
        DWM_TNP_OPACITY = 0x00000004,
        /// <summary>
        /// A value for the fVisible member has been specified.
        /// </summary>
        DWM_TNP_VISIBLE = 0x00000008,
        /// <summary>
        /// A value for the fSourceClientAreaOnly member has been specified.
        /// </summary>
        DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010
    }

    /// <summary>
    /// Defines the margins of windows that have visual styles applied.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        /// <summary>
        /// Width of the left border that retains its size.
        /// </summary>
        public int leftWidth;
        /// <summary>
        /// Width of the right border that retains its size.
        /// </summary>
        public int rightWidth;
        /// <summary>
        /// Height of the top border that retains its size.
        /// </summary>
        public int topHeight;
        /// <summary>
        /// Height of the bottom border that retains its size.
        /// </summary>
        public int bottomHeight;
    }
}
