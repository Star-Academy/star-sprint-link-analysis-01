namespace TransactionVisualizer.Models.Transaction;

public class Transaction
{
    public long ID { get; set; }
    public Account SourceAcount { set; get; }
    public Account  DestiantionAccount { set; get; }
    public TransactionType TransactionType { set; get; }
    public decimal Amount { set; get; }
    public DateTime Date { set; get; }

    public Transaction(long id, Account sourceAcount, Account destiantionAccount, TransactionType transactionType, decimal amount, DateTime date)
    {
        ID = id;
        SourceAcount = sourceAcount;
        DestiantionAccount = destiantionAccount;
        TransactionType = transactionType;
        Amount = amount;
        Date = date;
    }
}