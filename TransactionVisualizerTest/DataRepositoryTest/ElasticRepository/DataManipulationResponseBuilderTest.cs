using FluentAssertions;
using TransactionVisualizer.DataRepository.ElasticRepository;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class DataManipulationResponseBuilderTest
{
    [Fact]
    public void Build_WithError_ReturnsDataManipulationResponseWithError()
    {
        // Arrange
        bool error = true;

        // Act
        var response = DataManipulationResponseBuilder.Build(error);

        // Assert
        response.Error.Should().BeTrue();
    }

    [Fact]
    public void Build_WithoutError_ReturnsDataManipulationResponseWithoutError()
    {
        // Arrange
        bool error = false;

        // Act
        var response = DataManipulationResponseBuilder.Build(error);

        // Assert
        response.Error.Should().BeFalse();
    }
}