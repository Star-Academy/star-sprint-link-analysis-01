using System;
using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Transaction;

public class Transaction
{
    [Key]
    public long ID { get; set; }
    public long SourceAccount { set; get; }
    public long  DestiantionAccount { set; get; }
    public TransactionType TransactionType { set; get; }
    public decimal Amount { set; get; }
    public DateTime Date { set; get; }

   
}