using Microsoft.AspNetCore.Mvc;
using Nest;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ParserModel;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Graph;
using TransactionVisualizer.Utility.ParserUtils;

namespace TransactionVisualizer.Controller;

[Route("[controller]/[action]")]
public class GraphController : Microsoft.AspNetCore.Mvc.Controller
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;

    EdgeRepository edgeRepository = new EdgeRepository();

    public GraphController(IGraphProcessor<Account, Transaction> graphProcessor)
    {
        _graphProcessor = graphProcessor;
    }


    [HttpGet]
    public ActionResult<string> Graph()
    {
        // Create a custom graph
        var graph = new CustomGraph<Account, Transaction>();

        EdgeRepository repository = new EdgeRepository();
        List<Edge<Account, Transaction>> edges = edgeRepository.Search(p => p.MatchAll());

        
        _graphProcessor.SetGraph(graph);
        edges.ForEach(item =>
        {
            _graphProcessor.GetGraph().AddEdge(item);
        });
        
        foreach (var keyValuePair in _graphProcessor.GetGraph().adjacencyMatrix)
        {
            Console.WriteLine(keyValuePair.Key.AccountID+"  =>  " + keyValuePair.Value.Count);
        }

        var sourceAccount = edges[3].Source;
        var sinkAccount = edges[6].Destination;
        
        Console.WriteLine($"finding all path for {sourceAccount.AccountID} => {sinkAccount.AccountID}");
        
        var path = _graphProcessor.GetAllPaths(sourceAccount, sinkAccount);

        path.ForEach(p =>
        {
            p.ForEach(item =>
                {
                    Console.Write(item.Source.AccountID + "=>" + item.Destination.AccountID + " " +
                                  item.EdgeContent.Amount + ",");
                }
            );
            Console.WriteLine();
        });

        var maxFlow = _graphProcessor.GetMaxFlow(sourceAccount, sinkAccount);
        Console.WriteLine(
            "Max flow from " + sourceAccount.AccountID + " to " + sinkAccount.AccountID + " is " + maxFlow);

        return new ActionResult<string>("Max flow from " + sourceAccount.AccountID + " to " + sinkAccount.AccountID +
                                        " is " + maxFlow);
    }


    [HttpGet]
    public ActionResult<string> ToElastic()
    {
        string accountPath =
            "/Users/mahdimazaheri/Programming/Mohaymen/TransactionVisualizer/TransactionVisualizerTest/Accounts.csv";
        string transactionPath =
            "/Users/mahdimazaheri/Programming/Mohaymen/TransactionVisualizer/TransactionVisualizerTest/Transaction.csv";

        var accountParser = new Parser<FlatAccount>();
        var transactionParser = new Parser<FlatTransaction>();

        var flatAccounts = accountParser.Pars(accountPath);
        var flatTransactions = transactionParser.Pars(transactionPath);

        List<Edge<Account, Transaction>> edges = new List<Edge<Account, Transaction>>();


        IFlatToFullConverter<Account, FlatAccount> accountFlatToFull = new FlatAccountToAccount();
        var account = accountFlatToFull.ConvertAll(flatAccounts);

        IFlatToFullConverter<Transaction, FlatTransaction> transactionFlatToFull = new FlatTransactionToTransaction();
        var transaction = transactionFlatToFull.ConvertAll(flatTransactions);
        transaction.ForEach(item =>
        {
            var edge = new Edge<Account, Transaction>
            {
                Source = account.Find(a => a.AccountID == item.SourceAccount),
                Destination = account.Find(a => a.AccountID == item.DestiantionAccount),
                EdgeContent = item,
                weight = item.Amount
            };
            edges.Add(edge);
        });

        edgeRepository.AddAll(edges);
        return "finish";
    }
}