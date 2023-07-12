using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services.Data;
using TransactionVisualizer.Services.Graph;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
using TransactionVisualizer.Utility.Builders.ResponseModelBuilder;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.FlatToFull;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;
using TransactionVisualizer.Utility.Parsers.FileParsers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEdgeBuilder<Account, Transaction>, EdgeBuilder<Account, Transaction>>();
builder.Services.AddSingleton<IGraphResponseModelBuilder, GraphResponseModelBuilder>();
builder.Services
    .AddSingleton<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>,
        GraphFullModelToGraph>();
builder.Services.AddSingleton<IModelToGraphEdge<Transaction, Account, Transaction>, TransactionToEdge>();
builder.Services.AddSingleton<IBankingTransactionNetworkService, BankingTransactionNetworkService>();
builder.Services.AddSingleton<ISelectorBuilder, SelectorBuilder>();
builder.Services.AddSingleton<ISelectorKeyValueBuilder, SelectorKeyValueBuilder>();
builder.Services.AddSingleton<IExpander<Account, Transaction>, Expander<Account, Transaction>>();
builder.Services.AddSingleton<IMaxFlowCalculator<Account, Transaction>, MaxFlowCalculator<Account, Transaction>>();
builder.Services.AddSingleton<IPathsFinder<Account, Transaction>, PathsFinder<Account, Transaction>>();
builder.Services.AddSingleton<IDataGainResponseBuilder<Account>, DataGainResponseBuilder<Account>>();
builder.Services.AddSingleton<IDataGainResponseBuilder<Transaction>, DataGainResponseBuilder<Transaction>>();
builder.Services.AddSingleton<IDataService, DataService>();

builder.Services.AddSingleton<IElasticDataRepositoryBuilder<Account>, AccountRepositoryBuilder>();
builder.Services.AddSingleton<IElasticDataRepositoryBuilder<Transaction>, TransactionRepositoryBuilder>();

builder.Services.AddSingleton<IFlatToFullConverter<Account, FlatAccount>, FlatAccountToAccountConverter>();
builder.Services
    .AddSingleton<IFlatToFullConverter<Transaction, FlatTransaction>, FlatTransactionToTransactionConverter>();
builder.Services.AddSingleton<IFileParser<FlatAccount>, CsvFileParser<FlatAccount>>();
builder.Services.AddSingleton<IFileParser<FlatTransaction>, CsvFileParser<FlatTransaction>>();
// builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();