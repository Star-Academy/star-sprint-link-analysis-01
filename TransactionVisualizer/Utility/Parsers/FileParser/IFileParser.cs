namespace TransactionVisualizer.Utility.Parsers.FileParser;

// Jalase مسیر نگیرد، ریدری چیزی بگیرد
public interface IFileParser<T>
{
    List<T>? Pars(string path);
}