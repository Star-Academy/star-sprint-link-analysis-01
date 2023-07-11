using System.ComponentModel.DataAnnotations;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;

namespace TransactionVisualizer.Models.BusinessModels.Transaction;

public class Transaction
{
    [Key] public long Id { get; init; }
    public long SourceAccount { get; init; }
    public long DestinationAccount { get; init; }
    public TransactionType TransactionType { get; init; }

    public decimal Amount { get; init; }

    // public DateTime Date { set; get; }
    public string Date { get; init; }

    public override bool Equals(object? obj)
    {
        var transaction = obj as Transaction;

        return transaction != null && transaction.Id == Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}