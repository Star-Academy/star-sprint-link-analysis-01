using FluentAssertions;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.UtilityTest.Graph;

public class PathsFinderTest
{
    [Fact]
    public void Find_ReturnsEmptyList_WhenNoPathsExist()
    {
        // Arrange
        var graph = new Graph<string, string>();
        var finder = new PathsFinder<string, string>();

        // Act
        var paths = finder.Find("A", "B", graph);

        // Assert
        paths.Should().BeEmpty();
    }

    [Fact]
    public void Find_ReturnsSinglePath_WhenOnlyOnePathExists()
    {
        // Arrange
        var graph = new Graph<string, string>();
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "B", Content = "1" });
        var finder = new PathsFinder<string, string>();

        // Act
        var paths = finder.Find("A", "B", graph);

        // Assert
        paths.Should().HaveCount(1);
        paths.First().Should().BeEquivalentTo(new List<Edge<string, string>>
        {
            new Edge<string, string> { Source = "A", Destination = "B", Content = "1" }
        });
    }

    [Fact]
    public void Find_ReturnsMultiplePaths_WhenMultiplePathsExist()
    {
        // Arrange
        var graph = new Graph<string, string>();
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "B", Content = "1" });
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "C", Content = "2" });
        graph.AddEdge(new Edge<string, string> { Source = "C", Destination = "B", Content = "3" });
        var finder = new PathsFinder<string, string>();

        // Act
        var paths = finder.Find("A", "B", graph);

        // Assert
        paths.Should().HaveCount(2);
        paths.Should().ContainEquivalentOf(new List<Edge<string, string>>
        {
            new Edge<string, string> { Source = "A", Destination = "B", Content = "1" }
        });
        paths.Should().ContainEquivalentOf(new List<Edge<string, string>>
        {
            new Edge<string, string> { Source = "A", Destination = "C", Content = "2" },
            new Edge<string, string> { Source = "C", Destination = "B", Content = "3" }
        });
    }
}