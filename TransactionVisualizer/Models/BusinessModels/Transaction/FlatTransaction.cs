namespace TransactionVisualizer.Models.Transaction;

public class FlatTransaction
{
    public long SourceAcount { get; init; }
    public long DestiantionAccount { get; init; }
    public decimal Amount { get; init; }
    public string Date { get; init; }
    public long TransactionID { get; init; }
    public string Type { get; init; }
}