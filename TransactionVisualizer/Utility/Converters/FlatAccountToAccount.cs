using TransactionVisualizer.Exception;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;

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
            Branch = new Branch
            {
                Name = flatAccount.BranchName,
                Address = flatAccount.BranchAdress,
                Telephone = flatAccount.BranchTelephone
            },
            Owner = new Owner
            {
                ID = flatAccount.OwnerID,
                Name = flatAccount.OwnerName,
                FamilyName = flatAccount.OwnerFamilyName
            }
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
            case "پس انداز":
                return AccountType.Pasandaz;
            case "جاری":
                return AccountType.Jari;
            case "سپرده":
                return AccountType.Sepordeh;
        }

        throw new EnumParsException(accountType , nameof(AccountType));
    }


    
}