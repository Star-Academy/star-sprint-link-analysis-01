using System.Globalization;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransaction : IFlatToFullConverter<Models.Transaction.Transaction, FlatTransaction>
{
    public Models.Transaction.Transaction Convert(FlatTransaction flatTransaction)
    {
        string format = "yyyy/MM/dd";
        CultureInfo culture = new CultureInfo("fa-IR");

        return new Models.Transaction.Transaction
        {
            ID = flatTransaction.TransactionID,
            SourceAcount = flatTransaction.SourceAcount,
            DestiantionAccount = flatTransaction.DestiantionAccount,
            TransactionType = ParsTransactionType(flatTransaction.Type),
            Amount = flatTransaction.Amount,
            Date = DateTime.ParseExact(flatTransaction.Date, format, culture)
        };
    }
    
    

    public List<Models.Transaction.Transaction> ConvertAll(List<FlatTransaction> flatList)
    {
        List<Models.Transaction.Transaction> transactions = new List<Models.Transaction.Transaction>();
        flatList.ForEach(flat => transactions.Add(Convert(flat)));
        return transactions;
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