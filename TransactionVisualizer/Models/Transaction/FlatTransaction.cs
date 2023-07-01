namespace TransactionVisualizer.Models.Transaction;

public class FlatTransaction
{
    public long SourceAcount { get; set; }
    public long DestiantionAccount { get; set; }
    public decimal Amount { set; get; }
    public string Date { set; get; }
    public long TransactionID { get; set; }
    public string Type { get; set; }
}