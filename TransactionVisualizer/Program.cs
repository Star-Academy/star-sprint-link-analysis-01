// using TransactionVisualizer.DataRepository;
// using TransactionVisualizer.Models.BusinessLogicModels.Account;
// using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
// using TransactionVisualizer.Models.DataStructureModels.Graph.Builder;
// using TransactionVisualizer.Models.DataStructureModels.Graph.Graph;
// using TransactionVisualizer.Services;
//
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
//
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<IEdgeBuilder<Account, Transaction>, EdgeBuilder<Account, Transaction>>();
// builder.Services.AddSingleton<IGraphProcessor<Account, Transaction>, GraphProcessor<Account, Transaction>>();
// builder.Services.AddSingleton<IGraphService, GraphService>();
// builder.Services.AddSingleton<IEdgeRepository, EdgeRepository>();
//
// // builder.Services.AddSingleton<IGraphGenerator<Account, Transaction>, GraphGenerator>();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
//
// app.UseSwagger();
// app.UseSwaggerUI();
//
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();

using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.DataStructureModels.Graph.Graph;

class Program
{
    public static void Main(string[] args)
    {
        var edge1 = new Edge<string, string>() { Source = "0", Destination = "1", Content = "0->1", Weight = 11};
        var edge2 = new Edge<string, string>() { Source = "0", Destination = "2", Content = "0->2", Weight = 12};
        var edge3 = new Edge<string, string>() { Source = "1", Destination = "3", Content = "1->3", Weight = 12};
        var edge4 = new Edge<string, string>() { Source = "2", Destination = "1", Content = "2->1", Weight = 01};
        var edge5 = new Edge<string, string>() { Source = "2", Destination = "4", Content = "2->4", Weight = 11};
        var edge6 = new Edge<string, string>() { Source = "3", Destination = "5", Content = "3->5", Weight = 19};
        var edge7 = new Edge<string, string>() { Source = "4", Destination = "3", Content = "4->3", Weight = 07};
        var edge8 = new Edge<string, string>() { Source = "4", Destination = "5", Content = "4->5", Weight = 04};

        Graph<String, String> graph = new Graph<string, string>();
        graph.AddEdge(edge1);
        graph.AddEdge(edge2);
        graph.AddEdge(edge3);
        graph.AddEdge(edge4);
        graph.AddEdge(edge5);
        graph.AddEdge(edge6);
        graph.AddEdge(edge7);
        graph.AddEdge(edge8);

        GraphProcessor<String, String> graphProcessor = new GraphProcessor<string, string>();
        graphProcessor.SetGraph(graph);
        var path = graphProcessor.GetAllPaths("0", "5");
        decimal x = graphProcessor.GetMaxFlow("0", "5");
    }
}