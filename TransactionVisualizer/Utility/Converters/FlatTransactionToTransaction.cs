using System.Globalization;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransaction : IFlatToFullConverter<Models.Transaction.Transaction, FlatTransaction>
{
    private const string Format = "yyyy/MM/dd";
    private readonly CultureInfo _culture = new CultureInfo("fa-IR");

    public Transaction Convert(FlatTransaction flatTransaction)
    {
        return new Transaction
        {
            ID = flatTransaction.TransactionID,
            SourceAcount = flatTransaction.SourceAcount,
            DestiantionAccount = flatTransaction.DestiantionAccount,
            TransactionType = ParsTransactionType(flatTransaction.Type),
            Amount = flatTransaction.Amount,
            Date = DateTime.ParseExact(flatTransaction.Date, Format, _culture)
        };
    }


    public List<Transaction> ConvertAll(List<FlatTransaction> flatList)
    {
        return flatList.Select(Convert).ToList();
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

        throw new EnumParsException(transactionType , nameof(TransactionType));
    }
}