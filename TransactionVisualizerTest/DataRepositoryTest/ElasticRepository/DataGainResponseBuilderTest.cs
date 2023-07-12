using FluentAssertions;
using TransactionVisualizer.DataRepository.ElasticRepository;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class DataGainResponseBuilderTests
{
    private readonly DataGainResponseBuilder<int> _dataGainResponseBuilder = new DataGainResponseBuilder<int>();

    [Fact]
    public void Build_ShouldReturnDataGainResponseWithNoErrors_WhenItemsIsEmpty()
    {
        // Arrange
        var items = new List<int>();

        // Act
        var result = _dataGainResponseBuilder.Build(false, items);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeFalse();
        result.Items.Should().BeEmpty();
    }

    [Fact]
    public void Build_ShouldReturnDataGainResponseWithNoErrorsAndItems_WhenItemsIsNotEmpty()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 };

        // Act
        var result = _dataGainResponseBuilder.Build(false, items);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeFalse();
        result.Items.Should().NotBeEmpty().And.HaveCount(items.Count);
        result.Items.Should().ContainInOrder(items);
    }
}