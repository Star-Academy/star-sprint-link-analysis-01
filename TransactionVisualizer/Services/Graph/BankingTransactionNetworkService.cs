using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Services.Graph;

public class BankingTransactionNetworkService : IBankingTransactionNetworkService
{
    private readonly IDataRepository<Transaction> _transactionRepository;
    private readonly IExpander<Account, Transaction> _expander;
    private readonly IMaxFlowCalculator<Account, Transaction> _maxFlowCalculator;
    private readonly IModelToGraphEdge<Transaction, Account, Transaction> _modelToGraphEdge;

    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>
        _requestToFull;

    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;
    private Graph<Account, Transaction> _graph;

    public BankingTransactionNetworkService
    (
        IElasticDataRepositoryBuilder<Transaction> transactionRepositoryBuilder,
        IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>> requestToFull,
        IModelToGraphEdge<Transaction, Account, Transaction> modelToGraphEdge, IExpander<Account, Transaction> expander,
        IMaxFlowCalculator<Account, Transaction> maxFlowCalculator, ISelectorBuilder selectorBuilder,
        ISelectorKeyValueBuilder selectorKeyValueBuilder)
    {
        _graph = new Graph<Account, Transaction>();
        _transactionRepository = transactionRepositoryBuilder.Build();
        _requestToFull = requestToFull;
        _modelToGraphEdge = modelToGraphEdge;
        _expander = expander;
        _maxFlowCalculator = maxFlowCalculator;
        _selectorBuilder = selectorBuilder;
        _selectorKeyValueBuilder = selectorKeyValueBuilder;
    }


    public void SetState(GraphResponseModel<Account, Transaction> graph)
    {
        _graph = _requestToFull.Convert(graph);
    }

    public Graph<Account, Transaction> GetState()
    {
        return _graph;
    }

    public Graph<Account, Transaction> Expand(ExpandRequestModel<Account, Transaction> expandRequestModel)
    {
        return _expander.Expand(expandRequestModel.MaxLength, expandRequestModel.Vertex, _graph);
    }


    public decimal MaxFlowCalculator(MaxFlowCalculatorRequestModel<Account, Transaction> maxFlowCalculatorRequestModel)
    {
        return _maxFlowCalculator.Calculate
        (
            maxFlowCalculatorRequestModel.Source,
            maxFlowCalculatorRequestModel.Destenation,
            _graph
        );
    }

    public Graph<Account, Transaction> InitialGraph(long accountId)
    {
        var edges = _transactionRepository.Search(_selectorBuilder.BuildKeyValueSelector<Transaction>
            (
                _selectorKeyValueBuilder.BuildFindTransactionBySourceAccount
                (
                    accountId.ToString()
                )
            )
        );

        var graph = new Graph<Account, Transaction>();

        edges.Items.ForEach(item => graph.AddEdge(_modelToGraphEdge.Convert(item)));

        _graph = graph;

        return _graph;
    }
}