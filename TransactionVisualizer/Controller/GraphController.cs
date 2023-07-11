using Microsoft.AspNetCore.Mvc;
using TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;
using TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;
using TransactionVisualizer.Models.Account;
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
    private readonly IGraphProcessor<Account, Transaction> _graphProcessor;
    private readonly IGraphResponseModelBuilder _graphResponseModelBuilder;
    private readonly IGraphService _graphService;


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
        var graph = _graphService.InitialGraph(3000000037);
        return Ok(_graphResponseModelBuilder.BuildTransactionGraphResponseModel(graph.AdjacencyMatrix));
    }


    [HttpPost]
    [Route("/Expand")]
    public IActionResult Expand([FromBody] ExpandRequestModel<Account, Transaction> requestModel)
    {
        _graphService.SetState(requestModel.CurrentState);
        _graphService.Expand(requestModel.Vertex, requestModel.MaxLength);
        return Ok(_graphResponseModelBuilder.BuildTransactionGraphResponseModel(_graphProcessor.GetGraph()
            .AdjacencyMatrix));
    }

    [HttpPost]
    [Route("/Flow")]
    public IActionResult GetMaxFlow([FromBody] MaxFlowRequestModel<Account, Transaction> requestModel)
    {
        _graphService.SetState(requestModel.CurrentState);
        var maxFlow = _graphService.MaxFlow(requestModel);
        return Ok(new MaxFlowResponseModel { MaxFlow = maxFlow });
    }


    [HttpGet]
    [Route("/test")]
    public ActionResult<string> ToElastic()
    {
        var accountPath =
            "/Users/mahdimazaheri/Downloads/testData1/AccountaDB.csv";
        var transactionPath =
            "/Users/mahdimazaheri/Downloads/testData1/TransactionsDB (1).csv";

        var accountParser = new CsvFileParser<FlatAccount>();
        var transactionParser = new CsvFileParser<FlatTransaction>();

        var flatAccounts = accountParser.Pars(accountPath);
        Console.WriteLine("Flat account : " + flatAccounts.Count);
        var flatTransactions = transactionParser.Pars(transactionPath);
        Console.WriteLine("Flat account : " + flatTransactions.Count);


        IFlatToFullConverter<Account, FlatAccount> accountFlatToFull = new FlatAccountToAccountConverter();
        var account = accountFlatToFull.ConvertAll(flatAccounts);

        IFlatToFullConverter<Transaction, FlatTransaction> transactionFlatToFull =
            new FlatTransactionToTransactionConverter();
        var transaction = transactionFlatToFull.ConvertAll(flatTransactions);

        var transactionRepository = new TransactionRepository();
        transactionRepository.InsertAll(transaction);

        var accountRepository = new AccountRepository();
        accountRepository.InsertAll(account);


        return "finish";
    }
}