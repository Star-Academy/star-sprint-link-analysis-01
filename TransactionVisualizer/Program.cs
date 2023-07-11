using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;
using TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.ResponseModels.Builder;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
using TransactionVisualizer.Utility.Builders.ResponseModelBuilder;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
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
builder.Services.AddSingleton<IDataRepository<Account>, AccountRepository>();
builder.Services.AddSingleton<IDataRepository<Transaction>, TransactionRepository>();
builder.Services.AddSingleton<IGraphResponseModelBuilder, GraphResponseModelBuilder>();
builder.Services
    .AddSingleton<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>,
        GraphFullModelToGraph>();
builder.Services.AddSingleton<IDataRepository<Transaction>, TransactionRepository>();
builder.Services.AddSingleton<IModelToGraphEdge<Transaction, Account, Transaction>, TransactionToEdge>();
builder.Services.AddSingleton<IGraphService, GraphService>();
builder.Services.AddSingleton<ISelectorBuilder , SelectorBuilder>();
builder.Services.AddSingleton<ISelectorKeyValueBuilder, SelectorKeyValueBuilder>();
builder.Services.AddSingleton<IExpander<Account, Transaction>, Expander<Account, Transaction>>();
builder.Services.AddSingleton<IMaxFlowCalculator<Account, Transaction>, MaxFlowCalculator<Account, Transaction>>();
builder.Services.AddSingleton<IPathsFinder<Account, Transaction>, PathsFinder<Account, Transaction>>();



// builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();