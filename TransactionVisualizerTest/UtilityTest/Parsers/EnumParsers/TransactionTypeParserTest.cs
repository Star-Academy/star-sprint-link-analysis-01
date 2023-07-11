using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Constants.TransactionConstants;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.EnumParsers;

public class TransactionTypeParserTests
{
    [Fact]
    public void Parse_ShouldReturnSatna_WhenInputIsSatna()
    {
        // Arrange
        var input = TransactionTypeConstants.Satna;

        // Act
        var result = TransactionTypeParser.Pars(input);

        // Assert
        result.Should().Be(TransactionType.Satna);
    }

    [Fact]
    public void Parse_ShouldReturnPaya_WhenInputIsPaya()
    {
        // Arrange
        var input = TransactionTypeConstants.Paya;

        // Act
        var result = TransactionTypeParser.Pars(input);

        // Assert
        result.Should().Be(TransactionType.Paya);
    }

    [Fact]
    public void Parse_ShouldReturnKartBeKart_WhenInputIsKartBeKart()
    {
        // Arrange
        var input = TransactionTypeConstants.KartBeKart;

        // Act
        var result = TransactionTypeParser.Pars(input);

        // Assert
        result.Should().Be(TransactionType.KartBeKart);
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