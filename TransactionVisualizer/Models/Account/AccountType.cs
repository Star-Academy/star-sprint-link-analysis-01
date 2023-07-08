using TransactionVisualizer.Exception;
using static TransactionVisualizer.Utility.Constants.AccountRelatedConstants;

namespace TransactionVisualizer.Models.Account;

public enum AccountType
{
    Jari = 0,
    Sepordeh = 1,
    Pasandaz = 2
}

public static class AccountTypeExtensions
{
    public static AccountType ParsAccountType(this string accountType)
    {
        return accountType switch
        {
            Jari => AccountType.Jari,
            Sepordeh => AccountType.Sepordeh,
            Pasandaz => AccountType.Pasandaz,
            _ => throw new EnumParsException(accountType, nameof(AccountType))
        };
    }
}