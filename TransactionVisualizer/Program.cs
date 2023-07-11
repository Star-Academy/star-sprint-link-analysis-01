using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.EdgeRepository;
using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;
using TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Graph.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.ResponseModels.Builder;
using TransactionVisualizer.Models.Transaction;

using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEdgeBuilder<Account, Transaction>, EdgeBuilder<Account, Transaction>>();
builder.Services.AddSingleton<IModelRepository<Account>, AccountRepository>();
builder.Services.AddSingleton<IModelRepository<Edge<Account,Transaction>>, EdgeRepository>();
builder.Services.AddSingleton<IGraphResponseModelBuilder, GraphResponseModelBuilder>();
builder.Services.AddSingleton<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account , Transaction>>, GraphFullModelToGraph>();
builder.Services.AddSingleton<IModelRepository<Transaction> ,TransactionRepository>();
builder.Services.AddSingleton<IModelToGraphEdge<Transaction, Account, Transaction> , TransactionToEdge>();
builder.Services.AddSingleton<IGraphProcessor<Account, Transaction>, GraphProcessor<Account, Transaction>>();
builder.Services.AddSingleton<IGraphService, GraphService>();


// builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();