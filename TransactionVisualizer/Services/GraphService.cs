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
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Services;

public class GraphService : IGraphService
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;
    private IModelRepository<Transaction> _edgeRepository;
    private IModelToGraphEdge<Transaction, Account, Transaction> _modelToGraphEdge;

    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, CustomGraph<Account, Transaction>>
        _requestToFull;

    public GraphService(IGraphProcessor<Account, Transaction> graphProcessor,
        IModelRepository<Transaction> edgeRepository,
        IRequestToFullModel<GraphResponseModel<Account, Transaction>, CustomGraph<Account, Transaction>> requestToFull,
        IModelToGraphEdge<Transaction, Account, Transaction> modelToGraphEdge)
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

    public CustomGraph<Account, Transaction> GetState()
    {
        return _graphProcessor.GetGraph();
    }

    public CustomGraph<Account, Transaction> Expand(Account account, int maxLenght)
    {
        Stack<Account> stack = new Stack<Account>();
        stack.Push(account);
        _graphProcessor.LenghtExpand(maxLenght, stack, _edgeRepository);
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
                q.Match(m =>
                    m.Field(f =>
                            f.SourceAccount)
                        .Query(accountId.ToString()
                        )
                )
            )
        );

        var graph = new CustomGraph<Account, Transaction>();
        edges.ForEach(item => graph.AddEdge(_modelToGraphEdge.Convert(item)));

        _graphProcessor.SetGraph(graph);

        return _graphProcessor.GetGraph();
    }
}