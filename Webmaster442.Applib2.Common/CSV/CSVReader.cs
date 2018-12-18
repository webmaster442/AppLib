using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Webmaster442.Applib.CSV
{
    /// <summary>
    /// A simple CSV reader class
    /// </summary>
    public sealed class CSVReader: IDisposable, IEnumerable<CSVLine>
    {
        private TextReader _textReader;
        private readonly char _delimiter;

        /// <summary>
        /// Creates a new instance of CSV reader
        /// </summary>
        /// <param name="textReader">text reader source</param>
        /// <param name="delimiter">delimiter char of columns</param>
        public CSVReader(TextReader textReader, char delimiter = ';')
        {
            _textReader = textReader;
            _delimiter = delimiter;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_textReader != null)
            {
                _textReader.Dispose();
                _textReader = null;
            }
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public IEnumerator<CSVLine> GetEnumerator()
        {
            string line;
            while ((line = _textReader.ReadLine()) != null)
            {
                yield return new CSVLine(line, _delimiter);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
