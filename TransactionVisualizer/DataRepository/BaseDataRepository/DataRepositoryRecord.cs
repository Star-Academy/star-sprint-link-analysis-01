namespace TransactionVisualizer.DataRepository.BaseDataRepository;

public class DataRepositoryRecord<TResponse>
{
    public bool Error { get; set; }
    public TResponse? Item { get; set; }
}