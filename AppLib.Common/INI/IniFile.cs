using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppLib.Common.INI
{
    public class IniFile
    {
        private Dictionary<IniKey, string> _container;

        public string CurrentCategory { get; set; }

        public IniFile()
        {
            _container = new Dictionary<IniKey, string>();
        }

        public bool ContainsSetting(string cat, string setting)
        {
            var key = new IniKey(cat, setting);
            return _container.ContainsKey(key);
        }

        public string[] Categories
        {
            get
            {
                var q = from i in _container.Keys
                        select i.Category;
                return q.ToArray();
            }
        }

        public string this[string setting]
        {
            get { return GetSetting<string>(CurrentCategory, setting); }
            set { SetSetting<string>(CurrentCategory, setting, value); }
        }

        public string this[string category, string setting]
        {
            get { return GetSetting<string>(category, setting); }
            set { SetSetting<string>(category, setting, value); }
        }

        public T GetSetting<T>(string setting) where T: IConvertible
        {
            return GetSetting<T>(CurrentCategory, setting);
        }

        public T GetSetting<T>(string category, string setting) where T: IConvertible
        {
            var q = (from i in _container
                    where
                    i.Key.Category == category &&
                    i.Key.SettingName == setting
                    select i.Value).FirstOrDefault();

            if (q == null) return default(T);

            return (T)Convert.ChangeType(q, typeof(T));
        }

        public void SetSetting<T>(string setting, T value) where T : IConvertible
        {
            SetSetting<T>(CurrentCategory, setting, value);
        }

        public void SetSetting<T>(string category, string setting, T value) where T: IConvertible
        {
            var key = new IniKey(category, setting);
            var val = string.Format("\"{0}\"", value);
            if (_container.ContainsKey(key))
                _container[key] = val;
            else
                _container.Add(key, val);
        }

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
                    sb.AppendFormat("{0}=\"{1}\"\r\n", setting.Key.SettingName, setting.Value);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void SaveToStream(Stream target)
        {
            var s = Encoding.UTF8.GetBytes(SaveToString());
            target.Write(s, 0, s.Length);
        }

        public void ReadFromString(string s)
        {
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
                    var key = keypair[0];
                    var val = keypair[1].Substring(1, keypair[1].Length - 2);
                    this[key] = val;
                }
            }
        }

        public void ReadFromStream(Stream source)
        {
            var buffer = new byte[source.Length];
            source.Read(buffer, 0, buffer.Length);
            ReadFromString(Encoding.UTF8.GetString(buffer));
        }
    }
}
