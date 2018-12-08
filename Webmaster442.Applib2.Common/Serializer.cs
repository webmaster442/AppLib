using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace Webmaster442.Applib
{
    /// <summary>
    /// Compression Kind
    /// </summary>
    public enum CompressionKind
    {
        /// <summary>
        /// No compression
        /// </summary>
        None,
        /// <summary>
        /// Gzip compression
        /// </summary>
        Gzip,
    }

    public static class Serializer
    {
        /// <summary>
        /// Serialize an object to XML
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="target">Target stream</param>
        /// <param name="input">object to serialize</param>
        /// <param name="compressionKind">Compression kind</param>
        public static void SerializeXml<T>(Stream target, T input, CompressionKind compressionKind = CompressionKind.None)
        {
            Stream targetStream = target;
            if (compressionKind == CompressionKind.Gzip)
                targetStream = new GZipStream(target, CompressionLevel.Optimal, true);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(targetStream, input);

            if (compressionKind == CompressionKind.Gzip)
                targetStream.Close();
        }

        /// <summary>
        /// Serialize an object to JSON
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="target">Target stream</param>
        /// <param name="input">object to serialize</param>
        /// <param name="compressionKind">Compression kind</param>
        public static void SerializeJson<T>(Stream target, T input, CompressionKind compressionKind = CompressionKind.None)
        {
            Stream targetStream = target;
            if (compressionKind == CompressionKind.Gzip)
                targetStream = new GZipStream(target, CompressionLevel.Optimal, true);

            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            json.WriteObject(targetStream, input);

            if (compressionKind == CompressionKind.Gzip)
                targetStream.Close();
        }

        /// <summary>
        /// Deserialize an object from XML
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source stream</param>
        /// <param name="compressionKind">Compression kind</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeXml<T>(Stream source, CompressionKind compressionKind = CompressionKind.None)
        {
            Stream sourceStream = source;

            T result = default(T);

            if (compressionKind == CompressionKind.Gzip)
                sourceStream = new GZipStream(source, System.IO.Compression.CompressionMode.Decompress, true);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            result = (T)xs.Deserialize(sourceStream);

            if (compressionKind == CompressionKind.Gzip)
                sourceStream.Close();

            return result;
        }

        /// <summary>
        /// Deserialize an object from JSON
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source stream</param>
        /// <param name="compressionKind">Compression kind</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeJson<T>(Stream source, CompressionKind compressionKind = CompressionKind.None)
        {
            Stream sourceStream = source;

            T result = default(T);

            if (compressionKind == CompressionKind.Gzip)
                sourceStream = new GZipStream(source, System.IO.Compression.CompressionMode.Decompress, true);

            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            result = (T)json.ReadObject(sourceStream);

            if (compressionKind == CompressionKind.Gzip)
                sourceStream.Close();

            return result;
        }
    }
}
