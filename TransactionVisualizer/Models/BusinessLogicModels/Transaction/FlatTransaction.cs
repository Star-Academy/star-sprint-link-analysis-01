namespace TransactionVisualizer.Models.Transaction;

public class FlatTransaction
{
    public long SourceAccount { get; init; }
    public long DestinationAccount { get; init; }
    public decimal Amount { get; init; }
    public string Date { get; init; }
    public string Time { get; init; }
    public long TransactionID { get; init; }
    public string Type { get; init; }
}