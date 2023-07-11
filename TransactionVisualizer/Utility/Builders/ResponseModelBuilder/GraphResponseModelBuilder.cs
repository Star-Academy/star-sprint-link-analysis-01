using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.ResponseModels.Builder;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.ResponseModelBuilder;

public class GraphResponseModelBuilder : IGraphResponseModelBuilder
{
    public GraphResponseModel<Account, Transaction> BuildTransactionGraphResponseModel(
        Dictionary<Account, List<Edge<Account, Transaction>>> graph)
    {
        var vertices = new List<Account>();
        var edges = new List<EdgeResponseModel<Transaction>>();

        graph.Keys.ToList().ForEach(key => vertices.Add(key));
        graph.Values.ToList().ForEach(value => value.ForEach(edge => edges.Add(new EdgeResponseModel<Transaction>
                    {
                        Content = edge.Content,
                        Destination = edge.Destination.Id,
                        Source = edge.Source.Id
                    }
                )
            )
        );

        return new GraphResponseModel<Account, Transaction>
        {
            Vertices = vertices,
            Edges = edges,
            VertexCount = vertices.Count,
            EdgeCount = edges.Count
        };
    }
}