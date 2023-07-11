using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ResponseModels;

namespace TransactionVisualizer.Utility.Converters.RequestToFullModels;

public class GraphFullModelToGraph : IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account , Transaction>>
{
    private IModelRepository<Account> _repository;

    public GraphFullModelToGraph(IModelRepository<Account> repository)
    {
        _repository = repository;
    }

    public Graph<Account, Transaction> Convert(GraphResponseModel<Account, Transaction> request)
    {
        var graph = new Graph<Account, Transaction>();


        foreach (var edge in request.Edges)
        {
            var source = _repository.Search(descriptor =>
                descriptor.Query(q => q.Match(m =>
                        m.Field(f =>
                            f.Id).Query(edge.Source.ToString())
                    )
                )
            );
            var destination = _repository.Search(descriptor =>
                descriptor.Query(q => q.Match(m =>
                        m.Field(f =>
                            f.Id).Query(edge.Destination.ToString())
                    )
                )
            );

            graph.AddEdge(new Edge<Account, Transaction>()
            {
                Destination = destination[0], Source = source[0], Content = edge.Content,
                Weight = edge.Content.Amount
            });
            
        }

        return graph;
    }
}