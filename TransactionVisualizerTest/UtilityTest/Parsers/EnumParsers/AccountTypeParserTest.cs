using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Constants.AccountConstants;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.EnumParsers;

public class AccountTypeParserTests
{
    [Theory]
    [InlineData(AccountTypeConstants.Jari, AccountType.Jari)]
    [InlineData(AccountTypeConstants.Sepordeh, AccountType.Sepordeh)]
    [InlineData(AccountTypeConstants.Pasandaz, AccountType.Pasandaz)]
    public void Parse_ShouldReturnValidType_WhenValidInput(string input, AccountType accountTypeExpected)
    {
        // Act
        var result = AccountTypeParser.Pars(input);

        // Assert
        result.Should().Be(accountTypeExpected);
    }

    [Fact]
    public void Parse_ShouldThrowEnumParsException_WhenInputIsInvalid()
    {
        // Arrange
        var input = "InvalidAccountType";

        // Act
        Action act = () => AccountTypeParser.Pars(input);

        // Assert
        act.Should().ThrowExactly<EnumParsException>();
    }
}