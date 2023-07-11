using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Models.ResponseModels.Builder;

using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Models.Account;

public class GraphResponseModelBuilder : IGraphResponseModelBuilder
{
    public GraphResponseModel<BusinessLogicModels.Account.Account, BusinessModels.Transaction.Transaction> BuildTransactionGraphResponseModel(
        Dictionary<BusinessLogicModels.Account.Account, List<Edge<BusinessLogicModels.Account.Account, BusinessModels.Transaction.Transaction>>> graph)
    {
        var vertices = new List<BusinessLogicModels.Account.Account>();
        var edges = new List<EdgeResponseModel<BusinessModels.Transaction.Transaction>>();

        graph.Keys.ToList().ForEach(key => vertices.Add(key));
        graph.Values.ToList().ForEach(value => value.ForEach(edge => edges.Add(new EdgeResponseModel<BusinessModels.Transaction.Transaction>
                    {
                        Content = edge.Content,
                        Destination = edge.Destination.Id,
                        Source = edge.Source.Id
                    }
                )
            )
        );

        return new GraphResponseModel<BusinessLogicModels.Account.Account, BusinessModels.Transaction.Transaction>
        {
            Vertices = vertices,
            Edges = edges,
            VertexCount = vertices.Count,
            EdgeCount = edges.Count
        };
    }
}