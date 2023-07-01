namespace TransactionVisualizer.Models.Transaction;

public class Transaction
{
    public long ID { get; set; }
    public long SourceAcount { set; get; }
    public long  DestiantionAccount { set; get; }
    public TransactionType TransactionType { set; get; }
    public decimal Amount { set; get; }
    public DateTime Date { set; get; }

    public Transaction(long id, long sourceAcount, long destiantionAccount, TransactionType transactionType, decimal amount, DateTime date)
    {
        ID = id;
        SourceAcount = sourceAcount;
        DestiantionAccount = destiantionAccount;
        TransactionType = transactionType;
        Amount = amount;
        Date = date;
    }
}