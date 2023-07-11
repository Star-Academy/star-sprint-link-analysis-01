using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Services;

public class GraphService : IGraphService
{
    private readonly IDataRepository<Transaction> _edgeRepository;
    private readonly IGraphProcessor<Account, Transaction> _graphProcessor;
    private readonly IModelToGraphEdge<Transaction, Account, Transaction> _modelToGraphEdge;

    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>
        _requestToFull;


    public GraphService(
        IGraphProcessor<Account, Transaction> graphProcessor,
        IDataRepository<Transaction> edgeRepository,
        IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>> requestToFull,
        IModelToGraphEdge<Transaction, Account, Transaction> modelToGraphEdge
    )
    {
        _graphProcessor = graphProcessor;
        _edgeRepository = edgeRepository;
        _requestToFull = requestToFull;
        _modelToGraphEdge = modelToGraphEdge;
    }


    public void SetState(GraphResponseModel<Account, Transaction> graph)
    {
        _graphProcessor.SetGraph(_requestToFull.Convert(graph));
    }

    public Graph<Account, Transaction> GetState()
    {
        return _graphProcessor.GetGraph();
    }

    public Graph<Account, Transaction> Expand(Account account, int maxLenght)
    {
        var stack = new Stack<Account>();
        stack.Push(account);
        _graphProcessor.LenghtExpand(maxLenght, stack, _edgeRepository);
        return _graphProcessor.GetGraph();
    }


    public decimal MaxFlow(MaxFlowRequestModel<Account, Transaction> maxFlowRequestModel)
    {
        return _graphProcessor.GetMaxFlow(maxFlowRequestModel.Source, maxFlowRequestModel.Destenation);
    }

    public Graph<Account, Transaction> InitialGraph(long accountId)
    {
        var edges = _edgeRepository.Search(descriptor =>
            descriptor.Query(q =>
                q.Match(m =>
                    m.Field(f =>
                            f.SourceAccount)
                        .Query(accountId.ToString()
                        )
                )
            )
        );

        var graph = new Graph<Account, Transaction>();

        edges.Items.ForEach(item => graph.AddEdge(_modelToGraphEdge.Convert(item)));

        _graphProcessor.SetGraph(graph);

        return _graphProcessor.GetGraph();
    }
}