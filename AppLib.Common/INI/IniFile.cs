using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AppLib.Common.INI
{
    /// <summary>
    /// A Class for reading &amp; writing settings to INI files
    /// </summary>
    public class IniFile
    {
        private Dictionary<IniKey, string> _container;
        private CultureInfo _fileculture;

        /// <summary>
        /// Gets or sets current category section
        /// </summary>
        public string CurrentCategory { get; set; }

        /// <summary>
        /// Creates a new instance of IniFile. The underlying culture for formatting is enUS
        /// </summary>
        public IniFile()
        {
            _container = new Dictionary<IniKey, string>();
            _fileculture = CultureInfo.CreateSpecificCulture("en-US");
        }

        /// <summary>
        /// Creates a new instance of IniFile.
        /// </summary>
        /// <param name="culture">culture for formatting data</param>
        public IniFile(CultureInfo culture)
        {
            _container = new Dictionary<IniKey, string>();
            _fileculture = culture;
        }

        /// <summary>
        /// Checks, if the current file contains the specified setting
        /// </summary>
        /// <param name="cat">Category of the setting</param>
        /// <param name="setting">Setting to check</param>
        /// <returns>True, if the file contains the specified setting, otherwise false</returns>
        public bool ContainsSetting(string cat, string setting)
        {
            var key = new IniKey(cat, setting);
            return _container.ContainsKey(key);
        }

        /// <summary>
        /// Gets an array of categories in the file
        /// </summary>
        public string[] Categories
        {
            get
            {
                var q = from i in _container.Keys
                        select i.Category;
                return q.Distinct().ToArray();
            }
        }

        /// <summary>
        /// Gets or sets a setting value from the section specified by the CurrentCategory
        /// </summary>
        /// <param name="setting">Setting name</param>
        /// <returns>Setting value as string</returns>
        public string this[string setting]
        {
            get { return GetSetting<string>(CurrentCategory, setting); }
            set { SetSetting<string>(CurrentCategory, setting, value); }
        }

        /// <summary>
        /// Gets or sets a setting value.
        /// </summary>
        /// <param name="category">Category of the setting</param>
        /// <param name="setting">setting name</param>
        /// <returns>Setting value as string</returns>
        public string this[string category, string setting]
        {
            get { return GetSetting<string>(category, setting); }
            set { SetSetting<string>(category, setting, value); }
        }

        /// <summary>
        /// Gets a type converted setting value from the section specified by the CurrentCategory
        /// </summary>
        /// <typeparam name="T">Type of setting, must implement IConvertible</typeparam>
        /// <param name="setting">Setting name</param>
        /// <returns>type converted Setting value</returns>
        public T GetSetting<T>(string setting) where T: IConvertible
        {
            return GetSetting<T>(CurrentCategory, setting);
        }

        /// <summary>
        /// Gets a type converted setting value
        /// </summary>
        /// <typeparam name="T">Type of setting, must implement IConvertible</typeparam>
        /// <param name="category">Category of the setting</param>
        /// <param name="setting">setting name</param>
        /// <returns>type converted Setting value</returns>
        public T GetSetting<T>(string category, string setting) where T: IConvertible
        {
            var q = (from i in _container
                    where
                    i.Key.Category == category &&
                    i.Key.SettingName == setting
                    select i.Value).FirstOrDefault();

            if (q == null) return default(T);

            return (T)Convert.ChangeType(q, typeof(T), _fileculture);
        }

        /// <summary>
        /// Set a setting from a typed value in the section specified by the CurrentCategory
        /// </summary>
        /// <typeparam name="T">Type of setting, must implement IConvertible</typeparam>
        /// <param name="setting">setting name</param>
        /// <param name="value">Setting value</param>
        public void SetSetting<T>(string setting, T value) where T : IConvertible
        {
            SetSetting<T>(CurrentCategory, setting, value);
        }

        /// <summary>
        /// Set a setting from a typed value
        /// </summary>
        /// <typeparam name="T">Type of setting, must implement IConvertible</typeparam>
        /// <param name="category">setting category</param>
        /// <param name="setting">setting name</param>
        /// <param name="value">setting value</param>
        public void SetSetting<T>(string category, string setting, T value) where T: IConvertible
        {
            var key = new IniKey(category, setting);
            var val = string.Format("{0}", value.ToString(_fileculture));
            if (_container.ContainsKey(key))
                _container[key] = val;
            else
                _container.Add(key, val);
        }

        /// <summary>
        /// Saves the current IniFile instance to a string
        /// </summary>
        /// <returns>a string, representing the inifile</returns>
        public string SaveToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cat in Categories)
            {
                CurrentCategory = cat;
                sb.AppendFormat("[{0}]\r\n", CurrentCategory);
                var settings = from i in _container
                               where i.Key.Category == CurrentCategory
                               orderby i.Key.SettingName ascending
                               select i;
                foreach (var setting in settings)
                {
                    var key = setting.Key.SettingName;
                    var value = setting.Value.ToString(_fileculture);
                    sb.AppendFormat("{0}={1}\r\n", key, value);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Saves the current IniFile instance to a stream
        /// </summary>
        /// <param name="target">Target stream</param>
        public void SaveToStream(Stream target)
        {
            var s = Encoding.UTF8.GetBytes(SaveToString());
            target.Write(s, 0, s.Length);
        }

        /// <summary>
        /// Reads values from an string to the inifile
        /// </summary>
        /// <param name="s">a string, representing the inifile</param>
        /// <param name="apend">if set to true, the read vales are apended</param>
        public void ReadFromString(string s, bool apend = false)
        {
            if (!apend) Clear();
            string[] lines = s.Split('\n', '\r');
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    //category name
                    var cat = line.Substring(1, line.Length - 2);
                    CurrentCategory = cat;
                }
                else
                {
                    var keypair = line.Split('=');
                    var key = keypair[0].Trim();
                    var val = keypair[1].Trim();
                    this[key] = val;
                }
            }
        }

        /// <summary>
        /// Reads values from a stream to the inifile
        /// </summary>
        /// <param name="source">A stream to read from</param>
        /// <param name="apend">if set to true, the read vales are apended</param>
        private void ReadFromStream(Stream source, bool apend = false)
        {
            if (!apend) Clear();
            var buffer = new byte[source.Length];
            source.Read(buffer, 0, buffer.Length);
            ReadFromString(Encoding.UTF8.GetString(buffer));
        }

        /// <summary>
        /// Open an ini File from disk
        /// </summary>
        /// <param name="filename">File to open</param>
        /// <returns>An ini file instance</returns>
        public static IniFile Open(string filename)
        {
            IniFile ret = new IniFile();
            using (var f = File.OpenText(filename))
            {
                ret.ReadFromString(f.ReadToEnd());
            }
            return ret;
        }

        /// <summary>
        /// Open an ini file from stream
        /// </summary>
        /// <param name="source">stream source</param>
        /// <returns>an ini file instance</returns>
        public static IniFile Open(Stream source)
        {
            IniFile ret = new IniFile();
            ret.ReadFromStream(source);
            return ret;
        }

        /// <summary>
        /// Clears all settings &amp; associated values.
        /// </summary>
        public void Clear()
        {
            _container.Clear();
            CurrentCategory = "";
        }
    }
}
