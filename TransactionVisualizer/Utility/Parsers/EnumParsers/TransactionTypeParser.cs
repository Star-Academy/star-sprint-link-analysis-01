using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Utility.Constants.TransactionConstants;

namespace TransactionVisualizer.Utility.Parsers.EnumParsers;

public static class TransactionTypeParser
{
    public static TransactionType Pars(string transactionType)
    {
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