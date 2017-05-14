using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Update information class
    /// </summary>
    [Serializable]
    public class UpdateInfo
    {
        /// <summary>
        /// File path
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Publication Date
        /// </summary>
        [XmlAttribute("PublicationDate")]
        public DateTime PubDate { get; set; }
        /// <summary>
        /// Version Infrormation
        /// </summary>
        [XmlAttribute("Version")]
        public Version Version { get; set; }

        /// <summary>
        /// Converts current instance to text
        /// </summary>
        /// <returns>Current instance as string</returns>
        public override string ToString()
        {
            return string.Format("Update version: {0}, released: {1}", Version, PubDate);
        }
    }
}
