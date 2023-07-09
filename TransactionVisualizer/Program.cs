using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Utility;
using TransactionVisualizer.Utility.Builder;
using TransactionVisualizer.Utility.Graph;
using TransactionVisualizer.Utility.ParserUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEdgeBuilder<Account, Transaction>, EdgeBuilder<Account, Transaction>>();
builder.Services.AddSingleton<IGraphProcessor<Account, Transaction>, GraphProcessor<Account, Transaction>>();
builder.Services.AddSingleton<IGraphService, GraphService>();
builder.Services.AddSingleton<IEdgeRepository , EdgeRepository>();


builder.Services.AddTransient(p =>
{
    IDataRepository dataRepository = new ElasticDataRepository("http://localhost:9200", "transactions");
    return dataRepository;
});
// builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//
// using TransactionVisualizer.Models;
// using TransactionVisualizer.Models.Graph;
// using TransactionVisualizer.Models.Transaction;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using TransactionVisualizer.Models;
// using TransactionVisualizer.Models.Transaction;
// using TransactionVisualizer.Models.Graph;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using TransactionVisualizer.Models;
// using TransactionVisualizer.Models.Transaction;
// using TransactionVisualizer.Models.Graph;
// using TransactionVisualizer.Utility.Graph;
//
// public class Program
// {
//     public static void Main()
//     {
//         // Create sample data
//         var account1 = new Account { AccountID = 1, CardID = "123456789", Sheba = "IR123456789" };
//         var account2 = new Account { AccountID = 2, CardID = "987654321", Sheba = "IR987654321", };
//         var account3 = new Account { AccountID = 3, CardID = "456789123", Sheba = "IR456789123", };
//
//         var transaction1 = new Transaction
//             { ID = 1, SourceAccount = 1, DestiantionAccount = 2, Amount = 100, Date = DateTime.Now };
//         var transaction2 = new Transaction
//             { ID = 2, SourceAccount = 1, DestiantionAccount = 3, Amount = 50, Date = DateTime.Now };
//         var transaction3 = new Transaction
//             { ID = 3, SourceAccount = 2, DestiantionAccount = 3, Amount = 200, Date = DateTime.Now };
//
//         // Create a custom graph
//         var graph = new CustomGraph<Account, Transaction>();
//
//         // Add vertices and edges to the graph
//         graph.AddEdge(new Edge<Account, Transaction>
//             { Source = account1, Destination = account2, EdgeContent = transaction1, weight = 100 });
//         graph.AddEdge(new Edge<Account, Transaction>
//             { Source = account1, Destination = account3, EdgeContent = transaction2, weight = 50 });
//         graph.AddEdge(new Edge<Account, Transaction>
//             { Source = account2, Destination = account3, EdgeContent = transaction3, weight = 200 });
//
//         // Perform operations on the graph
//         var sourceAccount = account1;
//         var sinkAccount = account3;
//
//         GraphProcessor<Account , Transaction> graphProcessor = new GraphProcessor<Account, Transaction>(graph);
//
//         var path = graphProcessor.GetAllPaths(sourceAccount, sinkAccount);
//         path.ForEach(p =>
//         {
//             p.ForEach(item =>
//                 {
//                     Console.Write(item.Source.AccountID + "=>" + item.Destination.AccountID + " " +
//                                   item.EdgeContent.Amount + ",");
//                 }
//             );
//             Console.WriteLine();
//         });
//
//         var maxFlow = graphProcessor.GetMaxFlow(sourceAccount, sinkAccount);
//         Console.WriteLine(
//             "Max flow from " + sourceAccount.AccountID + " to " + sinkAccount.AccountID + " is " + maxFlow);
//     }
// }