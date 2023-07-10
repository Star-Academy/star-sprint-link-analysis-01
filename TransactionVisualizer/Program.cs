using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.EdgeRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph.Graph;
using TransactionVisualizer.Models.ResponseModels.Builder;
using TransactionVisualizer.Models.Transaction;

using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Builder;
using TransactionVisualizer.Utility.Graph;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEdgeBuilder<Account, Transaction>, EdgeBuilder<Account, Transaction>>();
builder.Services.AddSingleton<IGraphProcessor<Account, Transaction>, GraphProcessor<Account, Transaction>>();
builder.Services.AddSingleton<IGraphService, GraphService>();
builder.Services.AddSingleton<IEdgeRepository, EdgeRepository>();
builder.Services.AddSingleton<IGraphResponseModelBuilder, GraphResponseModelBuilder>();

// builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();