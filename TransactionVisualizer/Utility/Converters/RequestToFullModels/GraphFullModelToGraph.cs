using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;

namespace TransactionVisualizer.Utility.Converters.RequestToFullModels;

public class
    GraphFullModelToGraph : IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>
{
    private readonly IDataRepository<Account> _repository;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;

    public GraphFullModelToGraph(IDataRepository<Account> repository, ISelectorBuilder selectorBuilder,
        ISelectorKeyValueBuilder selectorKeyValueBuilder)
    {
        _repository = repository;
        _selectorBuilder = selectorBuilder;
        _selectorKeyValueBuilder = selectorKeyValueBuilder;
    }

    public Graph<Account, Transaction> Convert(GraphResponseModel<Account, Transaction> request)
    {
        var graph = new Graph<Account, Transaction>();


        foreach (var edge in request.Edges)
        {
            var source = _repository
                .Search(_selectorBuilder.BuildKeyValueSelector<Account>(
                    _selectorKeyValueBuilder.BuildFindAccountById(edge.Source.ToString()))).Items
                .First();


            var destination = _repository
                .Search(_selectorBuilder.BuildKeyValueSelector<Account>(
                    _selectorKeyValueBuilder.BuildFindAccountById(edge.Destination.ToString()))).Items
                .First();

            
            graph.AddEdge(new Edge<Account, Transaction>
            {
                Destination = destination, Source = source, Content = edge.Content,
                Weight = edge.Content.Amount
            });
        }

        return graph;
    }
}