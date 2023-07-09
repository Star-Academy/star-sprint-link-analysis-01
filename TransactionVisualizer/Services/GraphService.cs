using System.Collections;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Services;

public class GraphService : IGraphService
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;
    private IEdgeRepository _edgeRepository;

    public GraphService(IGraphProcessor<Account, Transaction> graphProcessor, IEdgeRepository edgeRepository)
    {
        _graphProcessor = graphProcessor;
        _edgeRepository = edgeRepository;
    }

    public void SetState(CustomGraph<Account, Transaction> graph)
    {
        _graphProcessor.SetGraph(graph);
    }

    public CustomGraph<Account, Transaction> GetState()
    {
        return _graphProcessor.GetGraph();
    }

    public CustomGraph<Account, Transaction> Expand(Account account, int MaxLenght)
    {
        Stack<Account> stack = new Stack<Account>();
        stack.Push(account);
        var edges = _edgeRepository.Search(descriptor => descriptor.MatchAll());
        _graphProcessor.LenghtExpand(MaxLenght, stack, edges);
        return _graphProcessor.GetGraph();
    }

    public decimal MaxFlow(Account source, Account destination)
    {
        return _graphProcessor.GetMaxFlow(source, destination);
    }

    public CustomGraph<Account, Transaction> InitialGraph(long accountId)
    {
        var edges = _edgeRepository.Search(descriptor =>
            descriptor.Query(q =>
                q.Match(queryDescriptor =>
                    queryDescriptor.Field(f =>
                        f.Source.AccountID).Query(accountId.ToString())
                )
            )
        );

        var graph = new CustomGraph<Account, Transaction>();
        edges.ForEach(item => graph.AddEdge(item));

        _graphProcessor.SetGraph(graph);

        return _graphProcessor.GetGraph();
    }
}