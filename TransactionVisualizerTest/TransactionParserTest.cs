using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility;
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
        string path = "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\Transaction.csv";
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
        string path = "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\Transaction.csv";
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

        Transaction transactionActual = FlatTransactionToTransaction.Convert(flatTransactions);
        Transaction transactionExcepted = new Transaction
        (153348811341,
            6534454617,
            6039548046,
            TransactionType.Paya,
            Decimal.Parse("500,000,000"),
            DateTime.Parse("1399/04/23")
        );

        // _out.WriteLine(accounts);
        Assert.Equivalent(transactionExcepted, transactionActual);
    }
}