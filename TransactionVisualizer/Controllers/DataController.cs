using Microsoft.AspNetCore.Mvc;
using TransactionVisualizer.Services.Data;
using FluentValidation;
namespace TransactionVisualizer.Controllers;

[Route("data/")]
public class DataController : Controller
{
    private IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var accountPath =
            "/Users/mahdimazaheri/Downloads/testData1/AccountaDB.csv";
        var transactionPath =
            "/Users/mahdimazaheri/Downloads/testData1/TransactionsDB (1).csv";
        
        var res = _dataService.AddAccounts(accountPath) && _dataService.AddTransactions(transactionPath);
        return res ? Ok("Done") : BadRequest("Error");
    }
}