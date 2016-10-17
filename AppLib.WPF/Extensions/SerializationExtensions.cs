using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Extensions for easy object serialization &amp; deserialization
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes a class instance to an xml file
        /// </summary>
        /// <typeparam name="T">Type of class to serialize</typeparam>
        /// <param name="Class">Class to serialize</param>
        /// <param name="TargetFile">Target file path</param>
        /// <remarks>Only classes with the attribute Serializable can be serialized</remarks>
        /// <seealso cref="SerializableAttribute"/>
        public static void SerializeXML<T>(this T Class, string TargetFile) where T : class
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
        public static string SerializeXML<T>(this T Class) where T : class
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
        /// Deserializes a class from an xml file
        /// </summary>
        /// <typeparam name="T">Type of class to deserialize</typeparam>
        /// <param name="Class">Class instance that provides type information</param>
        /// <param name="SourceFile">Source file path</param>
        /// <returns>A new object deserialized from the file with the type of T</returns>
        /// <remarks>Only classes with the attribute Serializable can be serialized</remarks>
        /// <seealso cref="SerializableAttribute"/>
        public static T DeSerializeXML<T>(this T Class, string SourceFile) where T: class
        {
            var type = Class.GetType();
            if (!type.IsSerializable) throw new ArgumentException("Only serializable classes can be deserialized", nameof(Class));
            XmlSerializer xs = new XmlSerializer(type);
            using (var fs = File.OpenRead(SourceFile))
            {
                return (T)xs.Deserialize(fs);
            }
        }


    }
}
