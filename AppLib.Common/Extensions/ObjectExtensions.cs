using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// Extensions for easy object serialization &amp; deserialization
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serializes a class instance to an xml file
        /// </summary>
        /// <typeparam name="T">Type of class to serialize</typeparam>
        /// <param name="Class">Class to serialize</param>
        /// <param name="TargetFile">Target file path</param>
        /// <remarks>Only classes with the attribute Serializable can be serialized</remarks>
        /// <seealso cref="SerializableAttribute"/>
        public static void SerializeXML<T>(this T Class, string TargetFile) where T : class, new()
        {
            var type = Class.GetType();
            if (!type.IsSerializable) throw new ArgumentException("Only serializable classes can be serialized", nameof(Class));
            XmlSerializer xs = new XmlSerializer(type);
            using (var fs = File.OpenWrite(TargetFile))
            {
                xs.Serialize(fs, Class);
            }
        }

        /// <summary>
        /// Serializes a class instance to an xml string
        /// </summary>
        /// <typeparam name="T">Type of class to serialize</typeparam>
        /// <param name="Class">Class to serialize</param>
        /// <returns>a string containing xml representation of the object</returns>
        public static string SerializeXML<T>(this T Class) where T : class, new()
        {
            var type = Class.GetType();
            if (!type.IsSerializable) throw new ArgumentException("Only serializable classes can be serialized", nameof(Class));
            XmlSerializer xs = new XmlSerializer(type);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xs.Serialize(xmlWriter, Class);
            return stringWriter.ToString();
        }


        /// <summary>
        /// Deserializes an object from an xml string
        /// </summary>
        /// <typeparam name="T">Type of class to serialize</typeparam>
        /// <param name="xml">xml data</param>
        /// <returns>an object with type of T, from the xml string</returns>
        public static T DeSerializeXML<T>(this string xml) where T : class, new()
        {

            if (string.IsNullOrEmpty(xml)) throw new ArgumentNullException("XML data not given", nameof(xml));

            XmlSerializer xs = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(xml))
            {
                return (T)xs.Deserialize(reader);
            }
        }

        /// <summary>
        /// Converts an IConvertible object to another type
        /// </summary>
        /// <typeparam name="T">Target type to convert to</typeparam>
        /// <param name="obj">IConvertible object to convert</param>
        /// <returns>The parameter converted to the target type</returns>
        public static T To<T>(this IConvertible obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Converts an IConvertible object to another type. If conversion fails then the default value is returned
        /// </summary>
        /// <typeparam name="T">Target type to convert to</typeparam>
        /// <param name="obj">IConvertible object to convert</param>
        /// <returns>The parameter converted to the target type</returns>
        public static T ToOrDefault<T>(this IConvertible obj)
        {
            try
            {
                return To<T>(obj);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Converts an IConvertible object to another type. If conversion fails then the default value is returned
        /// </summary>
        /// <typeparam name="T">Target type to convert to</typeparam>
        /// <param name="obj">IConvertible object to convert</param>
        /// <param name="defult">The default, fallback value</param>
        /// <returns>The parameter converted to the target type</returns>
        public static T ToOrDefault<T>(this IConvertible obj, T defult)
        {
            try
            {
                return To<T>(obj);
            }
            catch
            {
                return defult;
            }
        }

        /// <summary>
        /// Returns true, if the object is present in the parameter list
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">object reference</param>
        /// <param name="list">List of thing to serarch</param>
        /// <returns>true, if the object is found in the parameter list</returns>
        public static bool EqualsAnyOf<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }
    }
}
