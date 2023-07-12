using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Constants.TransactionConstants;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.EnumParsers;

public class TransactionTypeParserTests
{
    [Theory]
    [InlineData(TransactionTypeConstants.Satna, TransactionType.Satna)]
    [InlineData(TransactionTypeConstants.Paya, TransactionType.Paya)]
    [InlineData(TransactionTypeConstants.KartBeKart, TransactionType.KartBeKart)]
    public void Parse_ShouldReturnValidType_WhenValidInput(string input, TransactionType transactionTypeExpected)
    {
        // Act
        var result = TransactionTypeParser.Pars(input);

        // Assert
        result.Should().Be(transactionTypeExpected);
    }
    
    [Fact]
    public void Parse_ShouldThrowEnumParsException_WhenInputIsInvalid()
    {
        // Arrange
        var input = "InvalidTransactionType";

        // Act
        Action act = () => TransactionTypeParser.Pars(input);

        // Assert
        act.Should().ThrowExactly<EnumParsException>();
    }
}