using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility;
using TransactionVisualizer.Utility.Converters;
using Xunit.Abstractions;

namespace TransactionVisualizerTest;

public class TransactionParserTest
{
    private ITestOutputHelper _out;

    public TransactionParserTest(ITestOutputHelper @out)
    {
        _out = @out;
    }

    [Fact]
    public void ParseTransactionTest()
    {
        string path = "/Transaction.csv";
        path = GetFullPath.ConvertRelativeToAbsolute(path);
        IParser<FlatTransaction> iParser = new Parser<FlatTransaction>();
        FlatTransaction flatTransaction = new FlatTransaction
        {
            SourceAcount = 6534454617,
            DestiantionAccount = 6039548046,
            Amount = Decimal.Parse("500,000,000"),
            Date = "1399/04/23",
            TransactionID = 153348811341,
            Type = "پایا",
        };

        
        FlatTransaction flatTransactions = iParser.Pars(path)[0];

        // _out.WriteLine(accounts);
        Assert.Equivalent(flatTransactions, flatTransaction);
    }

    [Fact]
    public void FlatTransactionToTransactionTest()
    {
        string path = "/Transaction.csv";
        path = GetFullPath.ConvertRelativeToAbsolute(path);
        IParser<FlatTransaction> iParser = new Parser<FlatTransaction>();
        FlatTransaction flatTransaction = new FlatTransaction
        {
            SourceAcount = 6534454617,
            DestiantionAccount = 6039548046,
            Amount = Decimal.Parse("500,000,000"),
            Date = "1399/04/23",
            TransactionID = 153348811341,
            Type = "پایا",
        };

        FlatTransaction flatTransactions = iParser.Pars(path)[0];
        IFlatToFullConverter<Transaction , FlatTransaction> flatToFullConverter = new FlatTransactionToTransaction();
        Transaction transactionActual = flatToFullConverter.Convert(flatTransactions);
        Transaction transactionExcepted = new Transaction
            {ID = 153348811341,
            SourceAcount = 6534454617,
            DestiantionAccount = 6039548046,
            TransactionType = TransactionType.Paya,
            Amount = Decimal.Parse("500,000,000"),
            Date = DateTime.Parse("1399/04/23")
        };

        // _out.WriteLine(accounts);
        Assert.Equivalent(transactionExcepted, transactionActual);
    }
}