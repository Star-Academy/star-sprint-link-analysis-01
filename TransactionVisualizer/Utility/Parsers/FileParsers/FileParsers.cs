using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Parsers.FileParsers;

public class FileParsers
{
    public List<T>? Parse<T>(StreamReader reader, FileType type)
    {
        return type switch
        {
            FileType.Csv => new CsvFileParser<T>().Pars(reader),
            FileType.Json => new JsonFileParser<T>().Pars(reader),
            _ => throw new EnumParsException(reader.ReadLine(), nameof(TransactionType))
        };
    }
}