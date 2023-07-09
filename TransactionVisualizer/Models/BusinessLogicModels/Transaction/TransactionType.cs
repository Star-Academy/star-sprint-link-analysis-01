using TransactionVisualizer.Exception;
using static TransactionVisualizer.Utility.Constants.TransactionRelatedConstants;

namespace TransactionVisualizer.Models.BusinessLogicModels.Transaction;

public enum TransactionType
{
    Satna = 0,
    Paya = 1,
    KartBeKart = 2
}

// Jalase این اکستنشن روی کلاس استرینگ تعریف نشود و سعی بر برداشتن سویچ کیس شود
public static class TransactionTypeExtensions
{
    public static TransactionType ParsTransactionType(this string transactionType)
    {
        return transactionType switch
        {
            Satna => TransactionType.Satna,
            Paya => TransactionType.Paya,
            KartBeKart => TransactionType.KartBeKart,
            _ => throw new EnumParsException(transactionType, nameof(TransactionType))
        };
    }
}