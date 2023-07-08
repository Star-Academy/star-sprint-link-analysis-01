namespace TransactionVisualizer.Models.Account;

public class FlatAccount
{
    public long AccountId { get; init; }
    public string CardId { get; init; }
    public string Sheba { get; init; }
    public string AccountType { get; init; }
    public string BranchTelephone { get; init; }
    public string BranchAdress { get; init; }
    public string BranchName { get; init; }
    public string OwnerName { get; init; }
    public string OwnerFamilyName { get; init; }
    public long OwnerId { get; init; }
}