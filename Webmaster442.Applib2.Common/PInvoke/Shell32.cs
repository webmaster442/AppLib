﻿using System;
using System.Runtime.InteropServices;

namespace Webmaster442.Applib.PInvoke
{
    internal static class Shell32
    {
        [DllImport("shell32")]
        public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags);

    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    internal static class SHGetFileInfoConstants
    {
        public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
        public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
        public const uint FILE_ATTRIBUTE_VIRTUAL = 0x00010000;

        public const uint SHGFI_ICON = 0x000000100;     // get icon
        public const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
        public const uint SHGFI_TYPENAME = 0x000000400;     // get type name
        public const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
        public const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
        public const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
        public const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
        public const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
        public const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
        public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
        public const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
        public const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
        public const uint SHGFI_OPENICON = 0x000000002;     // get open icon
        public const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
        public const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute
    }
}
