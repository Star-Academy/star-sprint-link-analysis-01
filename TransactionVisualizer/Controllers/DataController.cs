using Microsoft.AspNetCore.Mvc;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Services.Data;

namespace TransactionVisualizer.Controllers;

[Route("data/")]
public class DataController : Controller
{
    private readonly IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpPost]
    [Route("accounts")]
    public IActionResult Accounts([FromBody] IndexDataRequestModel dataRequestModel)
    {
        var res = _dataService.AddAccounts(dataRequestModel.Path);
        return Ok(res);
    }

    [HttpPost]
    [Route("transactions")]
    public IActionResult Transaction([FromBody] IndexDataRequestModel dataRequestModel)
    {
        var accountPath =
            "/Users/mahdimazaheri/Downloads/testData1/AccountaDB.csv";
        var transactionPath =
            "/Users/mahdimazaheri/Downloads/testData1/TransactionsDB (1).csv";

        var res = _dataService.AddTransactions(dataRequestModel.Path);
        return Ok(res);
    }
}