using TransactionVisualizer.Models.BusinessLogicModels;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizer.Utility.Converters;

public class FlatAccountToAccountConverter : IFlatToFullConverter<Account, FlatAccount>
{
    public Account Convert(FlatAccount flat)
    {
        return new Account
        {
            AccountID = flat.AccountId,
            CardID = flat.CardId,
            Sheba = flat.Sheba,
            AccountType = AccountTypeParser.Pars(flat.AccountType),
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
            Id = flat.OwnerId,
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