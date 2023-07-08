using FluentAssertions;
using TransactionVisualizer.DataRepository.ElasticRepository;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class DataGainResponseBuilderTest
{
    [Fact]
    public void Build_WithErrorsAndItems_ReturnsDataGainResponseWithCorrectValues()
    {
        // Arrange
        var errors = true;
        var items = new List<string> { "Item 1", "Item 2", "Item 3" };

        // Act
        var response = DataGainResponseBuilder<string>.Build(errors, items);

        // Assert
        response.Error.Should().Be(errors);
        response.Items.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void Build_WithoutErrorsAndNullItems_ReturnsDataGainResponseWithCorrectValues()
    {
        // Arrange
        var errors = false;
        List<int>? items = null;

        // Act
        var response = DataGainResponseBuilder<int>.Build(errors, items);

        // Assert
        response.Error.Should().Be(errors);
        response.Items.Should().BeNull();
    }
}