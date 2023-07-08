using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Constants;

namespace TransactionVisualizerTest.ModelsTest.Account;

public class AccountTypeExtensionsTest
{
    [Fact]
    public void ParsAccountType_WithValidAccountType_ShouldReturnCorrectEnumValue()
    {
        // Arrange
        const string accountType = AccountRelatedConstants.Jari;

        // Act
        var result = accountType.ParsAccountType();

        // Assert
        result.Should().Be(AccountType.Jari);
    }

    [Fact]
    public void ParsAccountType_WithInvalidAccountType_ShouldThrowEnumParsException()
    {
        // Arrange
        const string accountType = "InvalidType";

        // Act
        Action action = () => accountType.ParsAccountType();

        // Assert
        action.Should().Throw<EnumParsException>();
    }
}
