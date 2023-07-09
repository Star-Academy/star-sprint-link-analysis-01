using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Database;

public class GraphTest
{
    [Theory]
    [InlineData(1, 100 , "A" , "D")]
    [InlineData(2, 80 , "A" , "V")]
    public void Graph1_Theory(int pathCount , int maxFlow , string source  , string destenation)
    {
        Edge<String, String> edge = new Edge<string, string>()
            { Destination = "B", Source = "A", EdgeContent = "AB", weight = 100 };
        Edge<String, String> edge2 = new Edge<string, string>()
            { Destination = "C", Source = "A", EdgeContent = "AC", weight = 100 };
        Edge<String, String> edge3 = new Edge<string, string>()
            { Destination = "D", Source = "C", EdgeContent = "CD", weight = 100 };
        Edge<String, String> edge4 = new Edge<string, string>()
            { Destination = "E", Source = "D", EdgeContent = "DE", weight = 100 };
        Edge<String, String> edge5 = new Edge<string, string>()
            { Destination = "E", Source = "A", EdgeContent = "DE", weight = 100 };
        Edge<String, String> edge6 = new Edge<string, string>()
            { Destination = "V", Source = "E", EdgeContent = "DE", weight = 40 };

        CustomGraph<String, String> graph = new CustomGraph<string, string>();
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