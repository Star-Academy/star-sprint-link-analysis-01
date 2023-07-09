using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransactionConverter : IFlatToFullConverter<Transaction, FlatTransaction>
{
    public Transaction Convert(FlatTransaction flat)
    {
        return new Transaction
        {
            ID = flat.TransactionId,
            SourceAccount = flat.SourceAccountId,
            DestiantionAccount = flat.DestinationAccountId,
            TransactionType = TransactionTypeParser.Pars(flat.Type),
            Amount = flat.Amount,
            Date = DateTimeParser.ParseExact(flat.Date)
        };
    }

    public List<Transaction> ConvertAll(IEnumerable<FlatTransaction> flats)
    {
        return flats.Select(Convert).ToList();
    }
}