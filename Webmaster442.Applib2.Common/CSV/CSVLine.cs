using System.Collections;
using System.Collections.Generic;

namespace Webmaster442.Applib.CSV
{
    /// <summary>
    /// Represents a Line of a CSV file
    /// </summary>
    public class CSVLine: IReadOnlyList<string>
    {
        private List<string> _row;

        /// <summary>
        /// Creates a new instance of CSV line
        /// </summary>
        /// <param name="sourceLine">Source RAW line</param>
        /// <param name="delimiter">CSV delimiter char</param>
        public CSVLine(string sourceLine, char delimiter) :base()
        {
            string[] parts = sourceLine.Split(delimiter);
            _row = new List<string>(parts);
        }

        /// <inheritdoc/>
        public string this[int index] => _row[index];

        /// <inheritdoc/>
        public int Count => _row.Count;

        /// <inheritdoc/>
        public IEnumerator<string> GetEnumerator()
        {
            return _row.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _row.GetEnumerator();
        }
    }
}
