namespace TransactionVisualizer.DataRepository;

public class DataGainResponse<TResponse>
{
    public bool Error { get; init; }

    public List<TResponse>? Items { get; init; }
}