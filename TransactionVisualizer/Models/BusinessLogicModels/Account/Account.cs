using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessLogicModels.Account;

public class Account
{
    [Key] public long AccountID { get; set; }
    public string CardID { get; set; }
    public string Sheba { get; set; }
    public AccountType AccountType { get; set; }
    public Branch Branch { get; set; }
    public Owner Owner { get; set; }

    public override bool Equals(object? obj)
    {
        var account = obj as Account;

        return account != null && account.AccountID == AccountID;
    }

    public override int GetHashCode()
    {
        return AccountID.GetHashCode();
    }
}