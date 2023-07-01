namespace TransactionVisualizer.Models;

public class Account
{
    public long AccountID { get; set; }
    public string CardID { get; set; }
    public string Sheba { get; set; }
    public AccountType AccountType { get; set; }
    public Branch Branch { get; set; }
    public Owner Owner { get; set; }

    public Account(long accountId, string cardId, string sheba, AccountType accountType, Branch branch, Owner owner)
    {
        AccountID = accountId;
        CardID = cardId;
        Sheba = sheba;
        AccountType = accountType;
        Branch = branch;
        Owner = owner;
    }
}