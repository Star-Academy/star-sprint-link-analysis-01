using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nest;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ParserModel;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Graph;
using TransactionVisualizer.Utility.ParserUtils;

namespace TransactionVisualizer.Controller;

[Route("graph/")]
public class GraphController : Microsoft.AspNetCore.Mvc.Controller
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;
    private IGraphService _graphService;

    public GraphController(IGraphProcessor<Account, Transaction> graphProcessor, IGraphService graphService)
    {
        _graphProcessor = graphProcessor;
        _graphService = graphService;
    }
    [HttpGet]
    [Route("")]
    public IActionResult Graph()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        options.Converters.Add(new AdjacencyMatrixConverter<Account, Transaction>());

        var jsonString = JsonSerializer.Serialize(_graphService.InitialGraph(6534454617), options);
        
        return Ok(jsonString);  
    }

    //
    // [HttpGet]
    // public ActionResult<string> ToElastic()
    // {
    //     string accountPath =
    //         "/Users/mahdimazaheri/Programming/Mohaymen/TransactionVisualizer/TransactionVisualizerTest/Accounts.csv";
    //     string transactionPath =
    //         "/Users/mahdimazaheri/Programming/Mohaymen/TransactionVisualizer/TransactionVisualizerTest/Transaction.csv";
    //
    //     var accountParser = new Parser<FlatAccount>();
    //     var transactionParser = new Parser<FlatTransaction>();
    //
    //     var flatAccounts = accountParser.Pars(accountPath);
    //     var flatTransactions = transactionParser.Pars(transactionPath);
    //
    //     List<Edge<Account, Transaction>> edges = new List<Edge<Account, Transaction>>();
    //
    //
    //     IFlatToFullConverter<Account, FlatAccount> accountFlatToFull = new FlatAccountToAccount();
    //     var account = accountFlatToFull.ConvertAll(flatAccounts);
    //
    //     IFlatToFullConverter<Transaction, FlatTransaction> transactionFlatToFull = new FlatTransactionToTransaction();
    //     var transaction = transactionFlatToFull.ConvertAll(flatTransactions);
    //     transaction.ForEach(item =>
    //     {
    //         var edge = new Edge<Account, Transaction>
    //         {
    //             Source = account.Find(a => a.AccountID == item.SourceAccount),
    //             Destination = account.Find(a => a.AccountID == item.DestiantionAccount),
    //             EdgeContent = item,
    //             weight = item.Amount
    //         };
    //         edges.Add(edge);
    //     });
    //
    //     edgeRepository.AddAll(edges);
    //     return "finish";
    // }
}