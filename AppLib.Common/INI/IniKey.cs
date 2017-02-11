using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.Common.INI
{
    public class IniKey
    {
        /// <summary>
        /// Category of the setting.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Name of the setting
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Creates a new instance of IniKey.
        /// </summary>
        /// <param name="setting">Specifies the setting name</param>
        public IniKey(string setting)
        {
            SettingName = setting;
            Category = "";
        }

        /// <summary>
        /// Creates a new instance of IniKey.
        /// </summary>
        /// <param name="cat">Specifies the category</param>
        /// <param name="setting">Specifies the setting name</param>
        public IniKey(string cat, string setting)
        {
            Category = cat;
            SettingName = setting;
        }

        /// <summary>
        /// Converts the key to a human readable string
        /// </summary>
        /// <returns>string representation of the object</returns>
        public override string ToString()
        {
            return string.Format("[{0}]\n{1}", Category, SettingName);
        }

        /// <summary>
        /// Converts the object to a hash value
        /// </summary>
        /// <returns>hash value</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ Category.GetHashCode();
                hash = (hash * 16777619) ^ SettingName.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Cheks object equality
        /// </summary>
        /// <param name="obj">a nother object</param>
        /// <returns>true, if the objects are equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            IniKey masik = obj as IniKey;
            if (masik == null) return false;
            return (masik.Category == Category) &&
                   (masik.SettingName == SettingName);
        }
    }
}
