using TransactionVisualizer.Exception;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;
using static TransactionVisualizer.Utility.Constants.AccountRelatedConstants;
namespace TransactionVisualizer.Utility.Converters;

public class FlatAccountToAccount : IFlatToFullConverter<Account , FlatAccount>
{
    public  Account Convert(FlatAccount flatAccount)
    {
        return new Account
        {
            AccountID = flatAccount.AccountID,
            CardID = flatAccount.CardID,
            Sheba = flatAccount.Sheba,
            AccountType = ParsAccountType(flatAccount.AccountType),
            Branch = ConvertBranch(flatAccount),
            Owner = ConvertOwner(flatAccount)
        };
    }

    private static Owner ConvertOwner(FlatAccount flatAccount)
    {
        return new Owner
        {
            ID = flatAccount.OwnerID,
            Name = flatAccount.OwnerName,
            FamilyName = flatAccount.OwnerFamilyName
        };
    }

    private static Branch ConvertBranch(FlatAccount flatAccount)
    {
        return new Branch
        {
            Name = flatAccount.BranchName,
            Address = flatAccount.BranchAdress,
            Telephone = flatAccount.BranchTelephone
        };
    }

    public List<Account> ConvertAll(List<FlatAccount> flatAccounts)
    {
        return  flatAccounts.Select(Convert).ToList();;
    }

    private  AccountType ParsAccountType(string accountType)
    {
        
        switch (accountType)
        {
            case PasAndaz:
                return AccountType.Pasandaz;
            case Jari:
                return AccountType.Jari;
            case Sepordeh:
                return AccountType.Sepordeh;
        }

        throw new EnumParsException(accountType , nameof(AccountType));
    }


    
}