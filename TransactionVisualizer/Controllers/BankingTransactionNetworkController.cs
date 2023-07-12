using Microsoft.AspNetCore.Mvc;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services.Graph;
using TransactionVisualizer.Utility.Builders.ResponseModelBuilder;

namespace TransactionVisualizer.Controllers;

[Route("banking-transaction-network/")]
public class BankingTransactionNetworkController : Controller
{
    private readonly IBankingTransactionNetworkService _bankingTransactionNetworkService;
    private readonly IGraphResponseModelBuilder _graphResponseModelBuilder;

    public BankingTransactionNetworkController
    (
        IBankingTransactionNetworkService bankingTransactionNetworkService,
        IGraphResponseModelBuilder graphResponseModelBuilder
    )
    {
        _bankingTransactionNetworkService = bankingTransactionNetworkService;
        _graphResponseModelBuilder = graphResponseModelBuilder;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Graph()
    {
        var graph = _bankingTransactionNetworkService.InitialGraph(3000000037);

        return Ok(_graphResponseModelBuilder.BuildTransactionGraphResponseModel(graph.AdjacencyMatrix));
    }


    [HttpPost]
    [Route("/expand")]
    public IActionResult Expand
    (
        [FromBody] ExpandRequestModel<Account, Transaction> requestModel
    )
    {
        _bankingTransactionNetworkService.SetState(requestModel.CurrentState);

        return Ok
        (
            _graphResponseModelBuilder.BuildTransactionGraphResponseModel
            (
                _bankingTransactionNetworkService.Expand(requestModel).AdjacencyMatrix
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
        _bankingTransactionNetworkService.SetState(calculatorRequestModel.CurrentState);

        var maxFlow = _bankingTransactionNetworkService.MaxFlowCalculator(calculatorRequestModel);

        return Ok
        (
            new MaxFlowResponseModel
            {
                MaxFlow = maxFlow
            }
        );
    }
}