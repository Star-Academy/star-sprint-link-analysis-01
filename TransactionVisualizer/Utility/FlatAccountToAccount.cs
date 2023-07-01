using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;

namespace TransactionVisualizerTest;

public class FlatAccountToAccount
{
    public static Account Convert(FlatAccount flatAccount)
    {
        return new Account(
            flatAccount.AccountID, 
            flatAccount.CardID,
            flatAccount.Sheba,
            ParsAccountType(flatAccount.AccountType),
            new Branch(flatAccount.BranchName,
                flatAccount.BranchAdress, 
                flatAccount.BranchTelephone),
            new Owner(flatAccount.OwnerID,
                flatAccount.OwnerName,
                flatAccount.OwnerFamilyName)
            );
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