using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizer.Utility.Converters.FlatToFull;
using Validator;

public class FlatTransactionToTransactionConverter : IFlatToFullConverter<Transaction, FlatTransaction>
{
    public Transaction Convert(FlatTransaction flat)
    {
        Validator.NullValidation(flat);
        
        return new Transaction
        {
            Id = flat.TransactionID,
            SourceAccount = flat.SourceAccount,
            DestinationAccount = flat.DestinationAccount,
            TransactionType = TransactionTypeParser.Pars(flat.Type),
            Amount = flat.Amount,
            Date = flat.Date
        };
    }

    public List<Transaction> ConvertAll(IEnumerable<FlatTransaction> flats)
    {
        Validator.ListValidation(flats.ToList());
        
        return flats.Select(Convert).ToList();
    }
}