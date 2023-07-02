using System.Globalization;
using TransactionVisualizer.Models.Transaction;
using Transaction = System.Transactions.Transaction;

namespace TransactionVisualizer.Utility;
using TransactionVisualizer.Models.Transaction;
public class FlatTransactionToTransaction
{
    public static Transaction Convert(FlatTransaction flatTransaction)
    {
        string format = "yyyy/MM/dd";
        CultureInfo culture = new CultureInfo("fa-IR");

        return new Transaction
        {
            ID = flatTransaction.TransactionID,
            SourceAcount = flatTransaction.SourceAcount,
            DestiantionAccount = flatTransaction.DestiantionAccount,
            TransactionType = ParsTransactionType(flatTransaction.Type),
            Amount = flatTransaction.Amount,
            Date = DateTime.ParseExact(flatTransaction.Date, format, culture)
        };

    }

    private static TransactionType ParsTransactionType(string transactionType)
    {
        switch (transactionType)
        {
            case "ساتنا":
                return TransactionType.Satna;
            case "پایا":
                return TransactionType.Paya;
            case "کارت به کارت":
                return TransactionType.KartBeKart;
        }

        return TransactionType.KartBeKart;
    }
}