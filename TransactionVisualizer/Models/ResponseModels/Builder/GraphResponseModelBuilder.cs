using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Models.ResponseModels.Builder;

using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Models.Account;

public class GraphResponseModelBuilder : IGraphResponseModelBuilder
{
    public GraphResponseModel<Account, BusinessModels.Transaction.Transaction> BuildTransactionGraphResponseModel(
        Dictionary<Account, List<Edge<Account, BusinessModels.Transaction.Transaction>>> graph)
    {
        var vertices = new List<Account>();
        var edges = new List<EdgeResponseModel<BusinessModels.Transaction.Transaction>>();

        graph.Keys.ToList().ForEach(key => vertices.Add(key));
        graph.Values.ToList().ForEach(value => value.ForEach(edge => edges.Add(new EdgeResponseModel<BusinessModels.Transaction.Transaction>
                    {
                        Content = edge.EdgeContent,
                        Destination = edge.Destination.Id,
                        Source = edge.Source.Id
                    }
                )
            )
        );

        return new GraphResponseModel<Account, BusinessModels.Transaction.Transaction>
        {
            Vertices = vertices,
            Edges = edges,
            VertexCount = vertices.Count,
            EdgeCount = edges.Count
        };
    }
}