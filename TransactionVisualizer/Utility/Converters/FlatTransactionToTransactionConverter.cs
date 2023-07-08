using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransactionConverter : IFlatToFullConverter<Transaction, FlatTransaction>
{
    public Transaction Convert(FlatTransaction flat)
    {
        return new Transaction
        {
            Id = flat.TransactionId,
            SourceAccountId = flat.SourceAccountId,
            DestinationAccountId = flat.DestinationAccountId,
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