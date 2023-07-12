using System.ComponentModel.DataAnnotations;
using Validator = TransactionVisualizer.Utility.Validator.Validator;

namespace TransactionVisualizer.Models.Transaction;

public class Transaction
{
    [Key] public long Id { get; init; }
    public long SourceAccount { get; init; }
    public long DestinationAccount { get; init; }
    public TransactionType TransactionType { get; init; }

    public decimal Amount { get; init; }

    public string Date { get; init; }

    public override bool Equals(object? obj)
    {
        if (Validator.TypeValidator(GetType(), obj.GetType()))
        {
            var transaction = obj as Transaction;
            return transaction != null && transaction.Id == Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}