using System.Globalization;
using CsvHelper;

namespace TransactionVisualizer.Utility.Parsers.FileParsers;

public class CsvFileParser<T> : IFileParser<T>
{
    public List<T> Pars(StreamReader reader)
    {
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<T>().ToList();
    }
}