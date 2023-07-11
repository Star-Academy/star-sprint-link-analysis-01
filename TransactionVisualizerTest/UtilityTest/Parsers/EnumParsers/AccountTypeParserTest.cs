using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Constants.AccountConstants;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.EnumParsers;

public class AccountTypeParserTests
{
    [Fact]
    public void Parse_ShouldReturnJari_WhenInputIsJari()
    {
        // Arrange
        var input = AccountTypeConstants.Jari;

        // Act
        var result = AccountTypeParser.Pars(input);

        // Assert
        result.Should().Be(AccountType.Jari);
    }

    [Fact]
    public void Parse_ShouldReturnSepordeh_WhenInputIsSepordeh()
    {
        // Arrange
        var input = AccountTypeConstants.Sepordeh;

        // Act
        var result = AccountTypeParser.Pars(input);

        // Assert
        result.Should().Be(AccountType.Sepordeh);
    }

    [Fact]
    public void Parse_ShouldReturnPasandaz_WhenInputIsPasandaz()
    {
        // Arrange
        var input = AccountTypeConstants.Pasandaz;

        // Act
        var result = AccountTypeParser.Pars(input);

        // Assert
        result.Should().Be(AccountType.Pasandaz);
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