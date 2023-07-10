using System.Collections;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.EdgeRepository;
using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Graph.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Services;

public class GraphService : IGraphService
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;
    private IModelRepository<Edge<Account, Transaction>> _edgeRepository;

    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, CustomGraph<Account, Transaction>>
        _requestToFull;

    public GraphService(IGraphProcessor<Account, Transaction> graphProcessor,
        IModelRepository<Edge<Account, Transaction>> edgeRepository,
        IRequestToFullModel<GraphResponseModel<Account, Transaction>, CustomGraph<Account, Transaction>> requestToFull)
    {
        _graphProcessor = graphProcessor;
        _edgeRepository = edgeRepository;
        _requestToFull = requestToFull;
    }

    public void SetState(GraphResponseModel<Account, Transaction> graph)
    {
        _graphProcessor.SetGraph(_requestToFull.Convert(graph));
    }

    public CustomGraph<Account, Transaction> GetState()
    {
        return _graphProcessor.GetGraph();
    }

    public CustomGraph<Account, Transaction> Expand(Account account, int maxLenght)
    {
        Stack<Account> stack = new Stack<Account>();
        stack.Push(account);
        _graphProcessor.LenghtExpand(maxLenght, stack, new EdgeRepository());
        return _graphProcessor.GetGraph();
    }


    public decimal MaxFlow(MaxFlowRequestModel<Account, Transaction> maxFlowRequestModel)
    {
        return _graphProcessor.GetMaxFlow(maxFlowRequestModel.Source, maxFlowRequestModel.Destenation);
    }

    public CustomGraph<Account, Transaction> InitialGraph(long accountId)
    {
        var edges = _edgeRepository.Search(descriptor =>
            descriptor.Query(q =>
                q.Match(queryDescriptor =>
                    queryDescriptor.Field(f =>
                        f.Source.Id).Query(accountId.ToString())
                )
            )
        );

        var graph = new CustomGraph<Account, Transaction>();
        edges.ForEach(item => graph.AddEdge(item));

        _graphProcessor.SetGraph(graph);

        return _graphProcessor.GetGraph();
    }
}