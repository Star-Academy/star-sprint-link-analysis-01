using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Branch;
using TransactionVisualizer.Models.Owner;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizer.Utility.Converters.FlatToFull;

using Validator;

public class FlatAccountToAccountConverter : IFlatToFullConverter<Account, FlatAccount>
{
    public Account Convert(FlatAccount flat)
    {
        Validator.NullValidation(flat);
        
        return new Account
        {
            Id = flat.AccountID,
            CardId = flat.CardID,
            Sheba = flat.Sheba,
            AccountType = AccountTypeParser.Pars(flat.AccountType),
            Branch = ConvertBranch(flat),
            Owner = ConvertOwner(flat)
        };
    }

    public List<Account> ConvertAll(IEnumerable<FlatAccount> flats)
    {
        Validator.ListValidation(flats.ToList());
        
        return flats.Select(Convert).ToList();
    }

    private static Owner ConvertOwner(FlatAccount flat)
    {
        Validator.NullValidation(flat);
        
        return new Owner
        {
            Id = flat.OwnerID,
            Name = flat.OwnerName,
            FamilyName = flat.OwnerFamilyName
        };
    }

    private static Branch ConvertBranch(FlatAccount flat)
    {
        Validator.NullValidation(flat);
        
        return new Branch
        {
            Name = flat.BranchName,
            Address = flat.BranchAdress,
            Telephone = flat.BranchTelephone
        };
    }
}