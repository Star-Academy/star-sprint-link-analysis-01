using System.ComponentModel.DataAnnotations;
using TransactionVisualizer.Models.BusinessModels;
using TransactionVisualizer.Models.BusinessModels.Account;

namespace TransactionVisualizer.Models.Account;

public class Account : BaseModel
{
    [Key] public long Id { get; set; }
    public string CardID { get; set; }
    public string Sheba { get; set; }
    public AccountType AccountType { get; set; }
    public Branch Branch { get; set; }
    public Owner Owner { get; set; }

    public override bool Equals(object? obj)
    {
        var account = obj as Account;
        return account != null && account.Id == Id;
    }

    public override string ToString()
    {
        return Id.ToString();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}