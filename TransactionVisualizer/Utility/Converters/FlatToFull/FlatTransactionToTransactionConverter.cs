using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizer.Utility.Converters;

public class FlatTransactionToTransactionConverter : IFlatToFullConverter<Transaction, FlatTransaction>
{
    public Transaction Convert(FlatTransaction flat)
    {
        return new Transaction
        {
            ID = flat.TransactionID,
            SourceAccount = flat.SourceAccount,
            DestiantionAccount = flat.DestinationAccount,
            TransactionType = TransactionTypeParser.Pars(flat.Type),
            Amount = flat.Amount,
            Date = flat.Date
        };
    }

    public List<Transaction> ConvertAll(IEnumerable<FlatTransaction> flats)
    {
        return flats.Select(Convert).ToList();
    }
}