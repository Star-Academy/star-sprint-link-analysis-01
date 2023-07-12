using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Constants.AccountConstants;

namespace TransactionVisualizer.Utility.Parsers.EnumParsers;

using Validator;

public static class AccountTypeParser
{
    public static AccountType Pars(string accountType)
    {
        Validator.NullValidation(accountType);
        
        // TODO: Using class AccountConstants instead of directly using class AccountTypeConstants 

        return accountType switch
        {
            AccountTypeConstants.Jari => AccountType.Jari,
            AccountTypeConstants.Sepordeh => AccountType.Sepordeh,
            AccountTypeConstants.Pasandaz => AccountType.Pasandaz,
            _ => throw new EnumParsException(accountType, nameof(AccountType))
        };
    }
}