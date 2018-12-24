using System.Collections.Generic;
using System.IO;

namespace CsvParser
{
    internal class CsvReader
    {
        private readonly string[] columns;
        private readonly TextReader source;

        public CsvReader(TextReader reader)
        {
            source = reader;
            var columnLine = reader.ReadLine();
            columns = columnLine.Split(',');
        }

        public IEnumerable<KeyValuePair<string, string>[]> Lines
        {
            get
            {
                string row;
                while ((row = source.ReadLine()) != null)
                {
                    var cells = row.Split(',');
                    var pairs = new KeyValuePair<string, string>[columns.Length];
                    for (var col = 0; col < columns.Length; col++)
                        pairs[col] = new KeyValuePair<string, string>(columns[col], cells[col]);
                    yield return pairs;
                }
            }
        }
    }
}