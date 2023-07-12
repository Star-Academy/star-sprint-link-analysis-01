using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Parsers.FileParsers;

using Validator;

public class FileParsers
{
    public List<T>? Parse<T>(string path, FileType type)
    {
        Validator.NullValidationGroup(path, type);
        
        return type switch
        {
            FileType.Csv => new CsvFileParser<T>().Pars(path),
            FileType.Json => new JsonFileParser<T>().Pars(path),
            _ => throw new EnumParsException(path, nameof(TransactionType))
        };
    }
}