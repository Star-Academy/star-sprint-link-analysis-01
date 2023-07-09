using FluentAssertions;
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
            SourceAccountId = 6534454617,
            DestinationAccountId = 6039548046,
            Amount = Decimal.Parse("500,000,000"),
            Date = "1399/04/23",
            TransactionId = 153348811341,
            Type = "پایا",
        };

        // Act
        var result = converter.Convert(flatTransaction);

        // Assert
        result.Should().NotBeNull();
        result.ID.Should().Be(flatTransaction.TransactionId);
        result.SourceAccount.Should().Be(flatTransaction.SourceAccountId);
        result.DestiantionAccount.Should().Be(flatTransaction.DestinationAccountId);
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
                SourceAccountId = 6534454617,
                DestinationAccountId = 6039548046,
                Amount = Decimal.Parse("500,000,000"),
                Date = "1399/04/23",
                TransactionId = 153348811341,
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
            transaction.ID.Should().Be(flatTransaction.TransactionId);
            transaction.SourceAccount.Should().Be(flatTransaction.SourceAccountId);
            transaction.DestiantionAccount.Should().Be(flatTransaction.DestinationAccountId);
            transaction.TransactionType.Should().Be(flatTransaction.Type.ParsTransactionType());
            transaction.Amount.Should().Be(flatTransaction.Amount);
            transaction.Date.Should().Be(DateTimeParser.ParseExact(flatTransaction.Date));
        }
    }
}