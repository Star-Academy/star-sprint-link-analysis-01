using FluentAssertions;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;

namespace TransactionVisualizerTest.UtilityTest.Converters;

public class FlatTransactionToTransactionConverterTest
{
    [Fact]
    public void Convert_ShouldConvertFlatTransactionToTransaction()
    {
        // Arrange
        var converter = new FlatTransactionToTransactionConverter();
        var flatTransaction = new FlatTransaction
        {
            SourceAcount = 6534454617,
            DestiantionAccount = 6039548046,
            Amount = Decimal.Parse("500,000,000"),
            Date = "1399/04/23",
            TransactionID = 153348811341,
            Type = "پایا",
        };

        // Act
        var result = converter.Convert(flatTransaction);

        // Assert
        result.Should().NotBeNull();
        result.ID.Should().Be(flatTransaction.TransactionID);
        result.SourceAccount.Should().Be(flatTransaction.SourceAcount);
        result.DestiantionAccount.Should().Be(flatTransaction.DestiantionAccount);
        result.TransactionType.Should().Be(TransactionType.Paya);
        result.Amount.Should().Be(flatTransaction.Amount);
        result.Date.Should().Be(DateTimeParser.ParseExact(flatTransaction.Date));
    }

    [Fact]
    public void ConvertAll_ShouldConvertAllFlatTransactionsToTransactions()
    {
        // Arrange
        var converter = new FlatTransactionToTransactionConverter();
        var flatTransactions = new List<FlatTransaction>
        {
            new FlatTransaction
            {
                SourceAcount = 6534454617,
                DestiantionAccount = 6039548046,
                Amount = Decimal.Parse("500,000,000"),
                Date = "1399/04/23",
                TransactionID = 153348811341,
                Type = "پایا",
            }
        };

        // Act
        var result = converter.ConvertAll(flatTransactions);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(flatTransactions.Count);

        foreach (var (flatTransaction, transaction) in flatTransactions.Zip(result, (f, t) => (f, t)))
        {
            transaction.ID.Should().Be(flatTransaction.TransactionID);
            transaction.SourceAccount.Should().Be(flatTransaction.SourceAcount);
            transaction.DestiantionAccount.Should().Be(flatTransaction.DestiantionAccount);
            transaction.TransactionType.Should().Be(flatTransaction.Type.ParsTransactionType());
            transaction.Amount.Should().Be(flatTransaction.Amount);
            transaction.Date.Should().Be(DateTimeParser.ParseExact(flatTransaction.Date));
        }
    }
}