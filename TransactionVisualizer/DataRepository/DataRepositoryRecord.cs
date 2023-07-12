namespace TransactionVisualizer.DataRepository;

public class DataRepositoryRecord<TResponse>
{
    public bool Error { get; set; }
    public TResponse? Item { get; set; }
}