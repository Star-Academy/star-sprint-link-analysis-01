using TransactionVisualizer.Exception;
using static TransactionVisualizer.Utility.Constants.AccountRelatedConstants;

namespace TransactionVisualizer.Models.BusinessLogicModels.Account;

public enum AccountType
{
    Jari = 0,
    Sepordeh = 1,
    Pasandaz = 2
}

// Jalase این اکستنشن روی کلاس استرینگ تعریف نشود و سعی بر برداشتن سویچ کیس شود
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