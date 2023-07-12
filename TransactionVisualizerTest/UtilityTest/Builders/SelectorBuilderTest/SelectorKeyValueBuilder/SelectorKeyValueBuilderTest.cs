using FluentAssertions;

namespace TransactionVisualizerTest.UtilityTest.Builders.SelectorBuilder.SelectorKeyValueBuilder;

public class SelectorKeyValueBuilderTests
{
    [Fact]
    public void BuildFindAccountById_ShouldReturnSelectorKeyValueWithCorrectProperties()
    {
        // Arrange
        var accountId = "123";
        var builder = new TransactionVisualizer.Utility.Builders.SelectorBuilder.SelectorKeyValueBuilder();

        // Act
        var selectorKeyValue = builder.BuildFindAccountById(accountId);

        // Assert
        selectorKeyValue.Should().NotBeNull();
        selectorKeyValue.Key.Should().Be("id");
        selectorKeyValue.Value.Should().Be(accountId);
    }

    [Fact]
    public void BuildFindTransactionBySourceAccount_ShouldReturnSelectorKeyValueWithCorrectProperties()
    {
        // Arrange
        var sourceAccountId = "456";
        var builder = new TransactionVisualizer.Utility.Builders.SelectorBuilder.SelectorKeyValueBuilder();

        // Act
        var selectorKeyValue = builder.BuildFindTransactionBySourceAccount(sourceAccountId);

        // Assert
        selectorKeyValue.Should().NotBeNull();
        selectorKeyValue.Key.Should().Be("sourceAccount");
        selectorKeyValue.Value.Should().Be(sourceAccountId);
    }
}