using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.DataStructureModels.Graph.Graph;

namespace TransactionVisualizer.Database;

public class GraphTest
{
    [Theory]
    [InlineData(1, 100 , "A" , "D")]
    [InlineData(2, 80 , "A" , "V")]
    public void Graph1_Theory(int pathCount , int maxFlow , string source  , string destenation)
    {
        Edge<String, String> edge = new Edge<string, string>()
            { Destination = "B", Source = "A", Content = "AB", Weight = 100 };
        Edge<String, String> edge2 = new Edge<string, string>()
            { Destination = "C", Source = "A", Content = "AC", Weight = 100 };
        Edge<String, String> edge3 = new Edge<string, string>()
            { Destination = "D", Source = "C", Content = "CD", Weight = 100 };
        Edge<String, String> edge4 = new Edge<string, string>()
            { Destination = "E", Source = "D", Content = "DE", Weight = 100 };
        Edge<String, String> edge5 = new Edge<string, string>()
            { Destination = "E", Source = "A", Content = "DE", Weight = 100 };
        Edge<String, String> edge6 = new Edge<string, string>()
            { Destination = "V", Source = "E", Content = "DE", Weight = 40 };

        Graph<String, String> graph = new Graph<string, string>();
        graph.AddEdge(edge);
        graph.AddEdge(edge2);
        graph.AddEdge(edge3);
        graph.AddEdge(edge4);
        graph.AddEdge(edge5);
        graph.AddEdge(edge6);

        GraphProcessor<String, String> graphProcessor = new GraphProcessor<string, string>();
        graphProcessor._graph = graph;
        var actualMaxFlow = graphProcessor.GetMaxFlow(source , destenation);
        var path = graphProcessor.GetAllPaths(source, destenation);
        Assert.Equal(maxFlow, actualMaxFlow);
        Assert.Equal(pathCount , path.Count );
    }
    
    
}