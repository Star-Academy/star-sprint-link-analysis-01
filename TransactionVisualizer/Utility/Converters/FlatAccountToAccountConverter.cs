using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels;
using TransactionVisualizer.Models.BusinessModels.Account;

namespace TransactionVisualizer.Utility.Converters;

public class FlatAccountToAccountConverter : IFlatToFullConverter<Account, FlatAccount>
{
    public Account Convert(FlatAccount flat)
    {
        return new Account
        {
            Id = flat.AccountID,
            CardID = flat.CardID,
            Sheba = flat.Sheba,
            AccountType = flat.AccountType.ParsAccountType(),
            Branch = ConvertBranch(flat),
            Owner = ConvertOwner(flat)
        };
    }

    public List<Account> ConvertAll(IEnumerable<FlatAccount> flats)
    {
        return flats.Select(Convert).ToList();
    }

    private static Owner ConvertOwner(FlatAccount flat)
    {
        return new Owner
        {
            ID = flat.OwnerID,
            Name = flat.OwnerName,
            FamilyName = flat.OwnerFamilyName
        };
    }

    private static Branch ConvertBranch(FlatAccount flat)
    {
        return new Branch
        {
            Name = flat.BranchName,
            Address = flat.BranchAdress,
            Telephone = flat.BranchTelephone
        };
    }
}