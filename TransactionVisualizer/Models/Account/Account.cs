using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Account;

public class Account
{
    [Key] public long AccountId { get; init; }
    public string CardId { get; set; }
    public string Sheba { get; set; }
    public AccountType AccountType { get; set; }
    public Branch Branch { get; set; }
    public Owner Owner { get; set; }
}