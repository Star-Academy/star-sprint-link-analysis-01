namespace TransactionVisualizerTest;

public interface IParser<T>
{
    List<T> Pars(string path);
}