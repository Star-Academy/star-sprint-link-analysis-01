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
        List<Account> accounts = new List<Account>();
        flatAccounts.ForEach(account => accounts.Add(Convert(account)));
        return accounts;
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

        return AccountType.Pasandaz;
    }


    
}