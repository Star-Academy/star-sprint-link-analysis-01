namespace TransactionVisualizer.DataRepository;

public class DataGainResponse<TResponse>
{
    public bool Error { get; init; }

    // Jalase برداشتن برگرداندن نال و لیست خالی برگرداند
    public List<TResponse> Items { get; init; }
}