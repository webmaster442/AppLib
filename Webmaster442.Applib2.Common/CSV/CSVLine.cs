using System.Collections;
using System.Collections.Generic;

namespace Webmaster442.Applib.CSV
{
    public class CSVLine: IReadOnlyList<string>
    {
        private List<string> _row;

        public CSVLine(string sourceLine, char delimiter) :base()
        {
            string[] parts = sourceLine.Split(delimiter);
            _row = new List<string>(_row);
        }

        public string this[int index] => _row[index];

        public int Count => _row.Count;

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
