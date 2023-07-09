namespace TransactionVisualizer.Models.BusinessLogicModels.Transaction;

public class FlatTransaction
{
    public long SourceAccountId { get; init; }
    public long DestinationAccountId { get; init; }
    public decimal Amount { get; init; }
    public string Date { get; init; }
    public long TransactionId { get; init; }
    public string Type { get; init; }
}