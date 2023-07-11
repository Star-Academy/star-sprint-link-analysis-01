using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Constants;
using TransactionVisualizer.Utility.Constants.TransactionConstants;

namespace TransactionVisualizerTest.ModelsTest.Transaction;

public class TransactionTypeExtensionsTest
{
    [Fact]
    public void ParsTransactionType_WithValidTransactionType_ShouldReturnCorrectEnumValue()
    {
        // Arrange
        const string transactionType = TransactionTypeConstants.Satna;

        // Act
        var result = transactionType.ParsTransactionType();

        // Assert
        result.Should().Be(TransactionType.Satna);
    }

    [Fact]
    public void ParsTransactionType_WithInvalidTransactionType_ShouldThrowEnumParsException()
    {
        // Arrange
        const string transactionType = "InvalidType";

        // Act
        Action action = () => transactionType.ParsTransactionType();

        // Assert
        action.Should().Throw<EnumParsException>();
    }
}