using System.Collections.Generic;

namespace TransactionVisualizer.Utility.ParserUtils;

public interface IParser<T>
{
    List<T> Pars(string path);
}