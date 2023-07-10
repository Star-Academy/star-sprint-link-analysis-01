using Microsoft.AspNetCore.Mvc;
using TransactionVisualizer.DataRepository.EdgeRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Graph.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.ResponseModels.Builder;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Graph;
using TransactionVisualizer.Utility.Parsers.FileParser;

namespace TransactionVisualizer.Controller;

[Route("graph/")]
public class GraphController : Microsoft.AspNetCore.Mvc.Controller
{
    private IGraphProcessor<Account, Transaction> _graphProcessor;
    private IGraphService _graphService;
    private IGraphResponseModelBuilder _graphResponseModelBuilder;

    public GraphController(
        IGraphProcessor<Account, Transaction> graphProcessor,
        IGraphService graphService,
        IGraphResponseModelBuilder graphResponseModelBuilder)
    {
        _graphProcessor = graphProcessor;
        _graphService = graphService;
        _graphResponseModelBuilder = graphResponseModelBuilder;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Graph()
    {
        var graph = _graphService.InitialGraph(6534454617);
        return Ok(_graphResponseModelBuilder.BuildTransactionGraphResponseModel(graph.AdjacencyMatrix));
    }


    [HttpPost]
    [Route("/Expand")]
    public IActionResult Expand([FromBody] ExpandRequestModel<Account, Transaction> requestModel)
    {
        _graphService.SetState(requestModel.CurrentState);
        var graph = _graphService.Expand(requestModel.Vertex, requestModel.MaxLength);
        return Ok(_graphResponseModelBuilder.BuildTransactionGraphResponseModel(graph.AdjacencyMatrix));
    }

    [HttpPost]
    [Route("/Flow")]
    public IActionResult GetMaxFlow([FromBody] MaxFlowRequestModel<Account, Transaction> requestModel)
    {
        _graphService.SetState(requestModel.CurrentState);
        var maxFlow = _graphService.MaxFlow(requestModel.Source, requestModel.Destenation);
        return Ok(maxFlow);
    }


    [HttpGet]
    [Route("/test")]
    public ActionResult<string> ToElastic()
    {
        string accountPath =
            "/Users/mahdimazaheri/Downloads/TestDB (1)/Accounts.csv";
        string transactionPath =
            "/Users/mahdimazaheri/Downloads/TestDB (1)/Transaction.csv";

        var accountParser = new CsvFileParser<FlatAccount>();
        var transactionParser = new CsvFileParser<FlatTransaction>();

        var flatAccounts = accountParser.Pars(accountPath);
        var flatTransactions = transactionParser.Pars(transactionPath);

        List<Edge<Account, Transaction>> edges = new List<Edge<Account, Transaction>>();


        IFlatToFullConverter<Account, FlatAccount> accountFlatToFull = new FlatAccountToAccountConverter();
        var account = accountFlatToFull.ConvertAll(flatAccounts);

        IFlatToFullConverter<Transaction, FlatTransaction> transactionFlatToFull =
            new FlatTransactionToTransactionConverter();
        var transaction = transactionFlatToFull.ConvertAll(flatTransactions);
        transaction.ForEach(item =>
        {
            var edge = new Edge<Account, Transaction>
            {
                Source = account.Find(a => a.Id == item.SourceAccount),
                Destination = account.Find(a => a.Id == item.DestiantionAccount),
                EdgeContent = item,
                weight = item.Amount
            };
            edges.Add(edge);
        });

        EdgeRepository edgeRepository = new EdgeRepository();

        edgeRepository.AddAll(edges);
        return "finish";
    }
}