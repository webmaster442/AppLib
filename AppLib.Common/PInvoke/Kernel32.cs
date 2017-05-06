using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.Common.PInvoke
{
    /// <summary>
    /// Platform Invokes to Kernel32.dll
    /// </summary>
    public static class Kernel32
    {
        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="lpFileName">
        /// The name of the module.This can be either a library module(a.dll file) or an executable module(an.exe file). The name specified is the file name of the module and is not related to the name stored in the library module itself, as specified by the LIBRARY keyword in the module-definition(.def) file.
        /// If the string specifies a full path, the function searches only that path for the module.
        /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module; for more information, see the Remarks.
        /// If the function cannot find the module, the function fails.When specifying a path, be sure to use backslashes (\), not forward slashes(/). For more information about paths, see Naming a File or Directory.
        /// If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name.To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the module.
        /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.
        /// /// </returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="lpFileName">
        /// A string that specifies the file name of the module to load.This name is not related to the name stored in a library module itself, as specified by the LIBRARY keyword in the module-definition(.def) file.
        /// The module can be a library module(a.dll file) or an executable module(an.exe file). If the specified module is an executable module, static imports are not loaded; instead, the module is loaded as if DONT_RESOLVE_DLL_REFERENCES was specified.See the dwFlags parameter for more information.
        /// If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name.To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
        /// If the string specifies a fully qualified path, the function searches only that path for the module.When specifying a path, be sure to use backslashes (\), not forward slashes(/). For more information about paths, see Naming Files, Paths, and Namespaces.
        /// If the string specifies a module name without a path and more than one loaded module has the same base name and extension, the function returns a handle to the module that was loaded first.
        /// If the string specifies a module name without a path and a module of the same name is not already loaded, or if the string specifies a module name with a relative path, the function searches for the specified module.The function also searches for modules if loading the specified module causes the system to load other associated modules (that is, if the module has dependencies). The directories that are searched and the order in which they are searched depend on the specified path and the dwFlags parameter.For more information, see Remarks.
        /// If the function cannot find the module or one of its dependencies, the function fails.
        /// </param>
        /// <param name="hReservedNull">This parameter is reserved for future use. It must be NULL.</param>
        /// <param name="dwFlags">The action to be taken when loading the module. If no flags are specified, the behavior of this function is identical to that of the LoadLibrary function.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the loaded module.
        /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. 
        /// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle
        /// is no longer valid.
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module. The LoadLibrary,
        /// LoadLibraryEx, GetModuleHandle, or GetModuleHandleEx function returns this handle.
        /// </param>
        /// <returns>If the function succeeds, the return value is true.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

    }
}
