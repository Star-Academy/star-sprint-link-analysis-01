using System.Globalization;
using CsvHelper;

namespace TransactionVisualizer.Utility.Parsers.FileParser;

public class CsvFileParser<T> : IFileParser<T>
{
    public List<T> Pars(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<T>().ToList();
    }
}