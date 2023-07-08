namespace TransactionVisualizer.Utility.Parsers.FileParser;

public interface IFileParser<T>
{
    List<T>? Pars(string path);
}