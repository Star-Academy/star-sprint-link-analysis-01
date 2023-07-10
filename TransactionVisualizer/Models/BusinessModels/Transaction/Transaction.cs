using System.ComponentModel.DataAnnotations;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Models.BusinessModels.Transaction;

public class Transaction
{
    [Key] public long ID { get; set; }
    public long SourceAccount { set; get; }
    public long DestiantionAccount { set; get; }
    public TransactionType TransactionType { set; get; }
    public decimal Amount { set; get; }
    public DateTime Date { set; get; }


    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Transaction transaction && transaction.ID == ID;
    }
}