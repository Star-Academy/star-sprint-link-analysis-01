using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Transaction;

public class Transaction
{
    [Key] public long Id { get; set; }
    public long SourceAccountId { get; init; }
    public long DestinationAccountId { get; init; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; init; }
}