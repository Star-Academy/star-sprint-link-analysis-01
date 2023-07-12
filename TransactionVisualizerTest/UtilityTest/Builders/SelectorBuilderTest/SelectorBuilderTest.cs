using FluentAssertions;
using Nest;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.UtilityTest.Builders.SelectorBuilderTest;

using TransactionVisualizer.Utility.Builders.SelectorBuilder;

public class SelectorBuilderTests
{
    [Fact]
    public void BuildKeyValueSelector_ShouldReturnCorrectSearchRequest()
    {
        // Arrange
        var builder = new SelectorBuilder();
        var keyValue = new SelectorKeyValue("id", "123");

        // Act
        Func<SearchDescriptor<Account>, ISearchRequest> selector = builder.BuildKeyValueSelector<Account>(keyValue);
        var searchDescriptor = new SearchDescriptor<Account>();
        var searchRequest = selector(searchDescriptor);

        // Assert
        searchRequest.Should().NotBeNull();
        searchRequest.Query.Should().NotBeNull();
    }
}
