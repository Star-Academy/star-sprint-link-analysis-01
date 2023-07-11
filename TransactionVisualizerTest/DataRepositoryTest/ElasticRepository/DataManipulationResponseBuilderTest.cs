using FluentAssertions;
using TransactionVisualizer.DataRepository.ElasticRepository;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class DataManipulationResponseBuilderTests
{
    [Fact]
    public void Build_ShouldReturnDataManipulationResponseWithNoErrors_WhenErrorIsFalse()
    {
        // Arrange
        var error = false;

        // Act
        var result = DataManipulationResponseBuilder.Build(error);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeFalse();
    }

    [Fact]
    public void Build_ShouldReturnDataManipulationResponseWithErrors_WhenErrorIsTrue()
    {
        // Arrange
        var error = true;

        // Act
        var result = DataManipulationResponseBuilder.Build(error);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeTrue();
    }
}