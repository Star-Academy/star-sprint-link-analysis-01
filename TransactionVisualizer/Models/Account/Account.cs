using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models;

public class Account
{
    [Key]
    public long AccountID { get; set; }
    public string CardID { get; set; }
    public string Sheba { get; set; }
    public AccountType AccountType { get; set; }
    public Branch Branch { get; set; }
    public Owner Owner { get; set; }
}