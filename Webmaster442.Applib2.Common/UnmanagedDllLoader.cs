using Webmaster442.Applib.PInvoke;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Webmaster442.Applib
{
    /// <summary>
    /// A class, that simplifies the dynamic loading of dll files
    /// </summary>
    public class UnmanagedDllLoader : IDisposable, IEnumerable<string>
    {
        private Dictionary<string, IntPtr> _modules;

        /// <summary>
        /// Creates a new instance of UnmanagedDllLoader
        /// </summary>
        public UnmanagedDllLoader()
        {
            _modules = new Dictionary<string, IntPtr>();
        }

        /// <summary>
        /// Loads a native DLL, if it hasn't been loaded yet.
        /// </summary>
        /// <param name="filename">Filename to be loaded. Can't be null or empty</param>
        public void Load(string filename)
        {
            if (_modules.ContainsKey(filename)) return;

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Filename can't be null or empty");

            var ptr = Kernel32.LoadLibrary(filename);

            if (ptr == IntPtr.Zero)
            {
                var lasterror = Marshal.GetLastWin32Error();
                var innerEx = new Win32Exception(lasterror);
                innerEx.Data.Add("LastWin32Error", lasterror);
                throw new Exception("can't load DLL " + filename, innerEx);
            }
            _modules.Add(filename, ptr);
        }

        /// <summary>
        /// Loads a native DLLs, The dll will only be loaded, if it hasn't been loaded yet.
        /// </summary>
        /// <param name="files">Files to load</param>
        public void Load(IEnumerable<string> files)
        {
            foreach (var file in files)
                Load(file);
        }

        /// <summary>
        /// Unloads a dll from the memory
        /// </summary>
        /// <param name="filename">File to be unloaded</param>
        public void UnLoad(string filename)
        {
            if (!_modules.ContainsKey(filename))
                throw new ArgumentException("File not found");
            var handle = _modules[filename];
            var result = Kernel32.FreeLibrary(handle);
            if (!result)
            {
                var lasterror = Marshal.GetLastWin32Error();
                var innerEx = new Win32Exception(lasterror);
                innerEx.Data.Add("LastWin32Error", lasterror);
                throw new Exception("can't unload DLL " + filename, innerEx);
            }
            _modules.Remove(filename);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="native">free native resources</param>
        protected virtual void Dispose(bool native)
        {
            foreach (var item in _modules)
            {
                if (item.Value != IntPtr.Zero)
                {
                    Kernel32.FreeLibrary(item.Value);
                }
            }
            _modules.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator`1 that can be used to iterate through the collection.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return _modules.Keys.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator`1 that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modules.Keys.GetEnumerator();
        }
    }
}
