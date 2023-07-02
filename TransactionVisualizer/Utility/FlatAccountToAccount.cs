using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;

namespace TransactionVisualizer.Utility;

public class FlatAccountToAccount
{
    public static Account Convert(FlatAccount flatAccount)
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

    private static AccountType ParsAccountType(string accountType)
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