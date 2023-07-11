using System.ComponentModel.DataAnnotations;
using TransactionVisualizer.Models.BusinessModels;

namespace TransactionVisualizer.Models.BusinessLogicModels.Account;

public class Account : BaseModel
{
    [Key] public long Id { get; init; }
    public string CardId { get; init; }
    public string Sheba { get; init; }
    public AccountType AccountType { get; init; }
    public Branch Branch { get; init; }
    public Owner Owner { get; init; }

    public override bool Equals(object? obj)
    {
        var account = obj as Account;

        return account != null && account.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}