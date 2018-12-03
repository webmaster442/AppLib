using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Webmaster442.Applib.CSV
{
    public sealed class CSVReader: IDisposable, IEnumerable<CSVLine>
    {
        private TextReader _textReader;
        private readonly char _delimiter;

        public CSVReader(TextReader textReader, char delimiter = ';')
        {
            _textReader = textReader;
            _delimiter = delimiter;
        }

        public void Dispose()
        {
            if (_textReader != null)
            {
                _textReader.Dispose();
                _textReader = null;
            }
            GC.SuppressFinalize(this);
        }

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
