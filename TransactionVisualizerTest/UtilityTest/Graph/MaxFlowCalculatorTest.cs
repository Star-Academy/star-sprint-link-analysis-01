using FluentAssertions;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.UtilityTest.Graph;

public class MaxFlowCalculatorTest
{
    [Fact]
    public void Calculate_ReturnsZero_WhenNoPathsExist()
    {
        // Arrange
        var graph = new Graph<string, string>();
        var pathsFinder = new PathsFinder<string, string>();
        var calculator = new MaxFlowCalculator<string, string>(pathsFinder);

        // Act
        var maxFlow = calculator.Calculate("A", "B", graph);

        // Assert
        maxFlow.Should().Be(0);
    }

    [Fact]
    public void Calculate_ReturnsCorrectResult_WhenPathsExist()
    {
        // Arrange
        var graph = new Graph<string, string>();
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "B", Content = "1", Weight = 5 });
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "B", Content = "2", Weight = 10 });
        graph.AddEdge(new Edge<string, string> { Source = "A", Destination = "C", Content = "3", Weight = 8 });
        graph.AddEdge(new Edge<string, string> { Source = "C", Destination = "B", Content = "4", Weight = 7 });
        var pathsFinder = new PathsFinder<string, string>();
        var calculator = new MaxFlowCalculator<string, string>(pathsFinder);

        // Act
        var maxFlow = calculator.Calculate("A", "B", graph);

        // Assert
        maxFlow.Should().Be(22);
    }
}