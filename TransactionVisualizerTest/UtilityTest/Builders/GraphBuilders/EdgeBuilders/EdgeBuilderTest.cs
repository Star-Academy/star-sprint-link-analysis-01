using FluentAssertions;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;

namespace TransactionVisualizerTest.UtilityTest.Builders.GraphBuilders.EdgeBuilders;

public class EdgeBuilderTests
{
    [Fact]
    public void Build_ShouldReturnEdgeWithCorrectProperties()
    {
        // Arrange
        var config = new EdgeConfig<string, string>
        {
            Source = "A",
            Destination = "B",
            Content = "Hello",
            Weight = 1
        };
        var builder = new EdgeBuilder<string, string>();

        // Act
        var edge = builder.Build(config);

        // Assert
        edge.Should().NotBeNull();
        edge.Source.Should().Be("A");
        edge.Destination.Should().Be("B");
        edge.Content.Should().Be("Hello");
        edge.Weight.Should().Be(1);
    }
}