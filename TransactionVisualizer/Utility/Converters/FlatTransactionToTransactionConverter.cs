using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransactionConverter : IFlatToFullConverter<Transaction, FlatTransaction>
{
    public Transaction Convert(FlatTransaction flat)
    {
        return new Transaction
        {
            ID = flat.TransactionID,
            SourceAccount = flat.SourceAcount,
            DestiantionAccount = flat.DestiantionAccount,
            TransactionType = flat.Type.ParsTransactionType(),
            Amount = flat.Amount,
            Date = DateTimeParser.ParseExact(flat.Date)
        };
    }

    public List<Transaction> ConvertAll(IEnumerable<FlatTransaction> flats)
    {
        return flats.Select(Convert).ToList();
    }
}