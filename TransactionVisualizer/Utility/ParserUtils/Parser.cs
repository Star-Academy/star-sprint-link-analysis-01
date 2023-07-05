using System.Globalization;
using CsvHelper;

namespace TransactionVisualizer.Utility.ParserUtils;

public class Parser<T> : IParser<T>
{
    public List<T> Pars(string path)
    {
        List<T> data = new List<T>();
        using (var reader = new StreamReader(path))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                data = csv.GetRecords<T>().ToList();
            }
            
        }

        
        return data;
    }
}