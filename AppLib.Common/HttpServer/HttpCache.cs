using AppLib.Common.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AppLib.Common.HttpServer
{
    /// <summary>
    /// Implements a simple HTTP cache
    /// </summary>
    public class HttpCache
    {
        private Dictionary<string, byte[]> _cache;
        private long _cachesize;

        /// <summary>
        /// Creates a new instance of the cache
        /// </summary>
        public HttpCache()
        {
            _cache = new Dictionary<string, byte[]>();
            _cachesize = 0;
        }

        /// <summary>
        /// Clears cache content
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
            _cachesize = 0;
        }

        /// <summary>
        /// Get the total number of cached files
        /// </summary>
        public int Count
        {
            get { return _cache.Count; }
        }

        /// <summary>
        /// Get the cache size in bytes
        /// </summary>
        public long Size
        {
            get { return _cachesize; }
        }

        /// <summary>
        /// Gets a cached file's contents
        /// </summary>
        /// <param name="name">Cached file name to retrive</param>
        /// <returns>the cached file content as a byte array</returns>
        public byte[] this[string name]
        {
            get { return _cache[name]; }
        }

        /// <summary>
        /// Searches a file name in the cache
        /// </summary>
        /// <param name="name">file name to search</param>
        /// <returns>true, if the file is cached, false if not.</returns>
        public bool Contains(string name)
        {
            return _cache.ContainsKey(name);
        }

        /// <summary>
        /// Adds a file to the cache
        /// </summary>
        /// <param name="filename">filename to add</param>
        public void Add(string filename)
        {
            var name = Path.GetFileName(filename);
            _cache.AddOrUpdate(name, File.ReadAllBytes(filename));
            _cachesize += _cache[name].LongLength;
        }

    }
}
