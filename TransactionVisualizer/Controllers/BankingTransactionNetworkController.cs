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
using TransactionVisualizer.Utility.Parsers.FileParser;

namespace TransactionVisualizer.Controllers;

[Route("banking-transaction-network/")]
public class BankingTransactionNetworkController : Controller
{
    private readonly IGraphResponseModelBuilder _graphResponseModelBuilder;
    private readonly IGraphService _graphService;

    public BankingTransactionNetworkController
    (
        IGraphService graphService,
        IGraphResponseModelBuilder graphResponseModelBuilder
    )
    {
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
    [Route("/expand")]
    public IActionResult Expand
    (
        [FromBody] ExpandRequestModel<Account, Transaction> requestModel
    )
    {
        _graphService.SetState(requestModel.CurrentState);

        return Ok
        (
            _graphResponseModelBuilder.BuildTransactionGraphResponseModel
            (
                _graphService.Expand(requestModel).AdjacencyMatrix
            )
        );
    }

    [HttpPost]
    [Route("/max-flow-calculator")]
    public IActionResult GetMaxFlow
    (
        [FromBody] MaxFlowCalculatorRequestModel<Account, Transaction> calculatorRequestModel
    )
    {
        _graphService.SetState(calculatorRequestModel.CurrentState);

        var maxFlow = _graphService.MaxFlowCalculator(calculatorRequestModel);

        return Ok
        (
            new MaxFlowResponseModel
            {
                MaxFlow = maxFlow
            }
        );
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