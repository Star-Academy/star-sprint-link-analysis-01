using FluentAssertions;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.ResponseModelBuilder;

namespace TransactionVisualizerTest.UtilityTest.Builders.ResponseModelBuilder;

public class GraphResponseModelBuilderTest
{
    [Fact]
    public void BuildTransactionGraphResponseModel_ReturnsCorrectModel_WhenGraphHasVerticesAndEdges()
    {
        // Arrange
        var graph = new Dictionary<Account, List<Edge<Account, Transaction>>>
        {
            {
                new Account { Id = 1, CardId = "1234" },
                new List<Edge<Account, Transaction>>
                {
                    new Edge<Account, Transaction>
                    {
                        Source = new Account { Id = 1, CardId = "1234" },
                        Destination = new Account { Id = 2, CardId = "5678" },
                        Content = new Transaction { Id = 1, Amount = 100 }
                    },
                    new Edge<Account, Transaction>
                    {
                        Source = new Account { Id = 1, CardId = "1234" },
                        Destination = new Account { Id = 3, CardId = "9012" },
                        Content = new Transaction { Id = 2, Amount = 200 }
                    }
                }
            },
            {
                new Account { Id = 2, CardId = "5678" },
                new List<Edge<Account, Transaction>>
                {
                    new Edge<Account, Transaction>
                    {
                        Source = new Account { Id = 2, CardId = "5678" },
                        Destination = new Account { Id = 3, CardId = "9012" },
                        Content = new Transaction { Id = 3, Amount = 300 }
                    }
                }
            }
        };
        var builder = new GraphResponseModelBuilder();

        // Act
        var model = builder.BuildTransactionGraphResponseModel(graph);

        // Assert
        model.VertexCount.Should().Be(3);
        model.EdgeCount.Should().Be(3);
        model.Should().BeEquivalentTo
        (new List<Account>()
            {
                new Account { Id = 1, CardId = "1234" },
                new Account { Id = 2, CardId = "5678" },
                new Account { Id = 3, CardId = "9012" }
            },
            options => options.WithStrictOrdering()
        );
        model.Should().BeEquivalentTo
        (new List<EdgeResponseModel<Transaction>>()
            {
                new EdgeResponseModel<Transaction>
                {
                    Source = 1,
                    Destination = 2,
                    Content = new Transaction { Id = 1, Amount = 100 }
                },
                new EdgeResponseModel<Transaction>
                {
                    Source = 1,
                    Destination = 3,
                    Content = new Transaction { Id = 2, Amount = 200 }
                },
                new EdgeResponseModel<Transaction>
                {
                    Source = 2,
                    Destination = 3,
                    Content = new Transaction { Id = 3, Amount = 300 }
                }
            },
            options => options.WithStrictOrdering()
        );
    }

    [Fact]
    public void BuildTransactionGraphResponseModel_ReturnsEmptyModel_WhenGraphHasNoVerticesOrEdges()
    {
        // Arrange
        var graph = new Dictionary<Account, List<Edge<Account, Transaction>>>();
        var builder = new GraphResponseModelBuilder();

        // Act
        var model = builder.BuildTransactionGraphResponseModel(graph);

        // Assert
        model.VertexCount.Should().Be(0);
        model.EdgeCount.Should().Be(0);
        model.Vertices.Should().BeEmpty();
        model.Edges.Should().BeEmpty();
    }
}