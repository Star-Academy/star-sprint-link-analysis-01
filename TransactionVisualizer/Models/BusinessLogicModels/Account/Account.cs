using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Account;

public class Account
{
    [Key] public long Id { get; init; }
    public string CardId { get; init; }
    public string Sheba { get; init; }
    public AccountType AccountType { get; init; }
    public Branch.Branch Branch { get; init; }
    public Owner.Owner Owner { get; init; }

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