using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Constants.TransactionConstants;

namespace TransactionVisualizer.Utility.Parsers.EnumParsers;

using Validator;

public static class TransactionTypeParser
{
    public static TransactionType Pars(string transactionType)
    {
        Validator.NullValidation(transactionType);
        
        // TODO: Using class TransactionConstants instead of directly using class TransactionTypeConstants 

        return transactionType switch
        {
            TransactionTypeConstants.Satna => TransactionType.Satna,
            TransactionTypeConstants.Paya => TransactionType.Paya,
            TransactionTypeConstants.KartBeKart => TransactionType.KartBeKart,
            _ => throw new EnumParsException(transactionType, nameof(TransactionType))
        };
    }
}