using System;
using System.IO;
using System.Text;
using Webmaster442.Applib.PInvoke;

namespace Webmaster442.Applib
{
    /// <summary>
    /// An INI file manipulation program
    /// </summary>
    public sealed class IniFile
    {
        /// <summary>
        /// Currently opened ini path
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Creates a new Instance of IniFile
        /// </summary>
        /// <param name="path">Ini file path. File must exist</param>
        public IniFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            Path = path;
        }

        /// <summary>
        /// Write a value to Ini file
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="key">Key</param>
        /// <param name="value">value</param>
        public void WriteValue(string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentNullException(nameof(section));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Kernel32.WritePrivateProfileString(section, key, value, Path);
        }

        /// <summary>
        /// Write a value to Ini file
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="key">Key</param>
        /// <param name="value">value</param>
        public void WriteValue(string section, string key, object value)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentNullException(nameof(section));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Kernel32.WritePrivateProfileString(section, key, value.ToString(), Path);
        }

        /// <summary>
        /// Read value from ini file
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>key value</returns>
        public string ReadValue(string section, string key)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentNullException(nameof(section));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            StringBuilder temp = new StringBuilder(1024);

            Kernel32.GetPrivateProfileString(section, key, "", temp,  1024, Path);

            return temp.ToString();
        }
    }
}
