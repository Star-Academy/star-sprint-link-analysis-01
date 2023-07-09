using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Database;

public class GraphTest
{
    [Fact]
    public void GetAllPath_Test()
    {
        Edge<String, String> edge = new Edge<string, string>()
            { Destination = "B", Source = "A", EdgeContent = "AB", weight = 100 };
        Edge<String, String> edge2 = new Edge<string, string>()
            { Destination = "C", Source = "A", EdgeContent = "AC", weight = 100 };
        Edge<String, String> edge3 = new Edge<string, string>()
            { Destination = "D", Source = "B", EdgeContent = "BD", weight = 100 };
        Edge<String, String> edge4 = new Edge<string, string>()
            { Destination = "E", Source = "B", EdgeContent = "BE", weight = 100 };

        CustomGraph<String, String> graph = new CustomGraph<string, string>();
        graph.AddEdge(edge);
        graph.AddEdge(edge2);
        graph.AddEdge(edge3);
        graph.AddEdge(edge4);

        GraphProcessor<String, String> graphProcessor = new GraphProcessor<string, string>();
        graphProcessor._graph = graph;
        var path = graphProcessor.GetAllPaths("A", "E");
        Assert.Equal(1, path.Count);
    }    [Fact]
    public void GetAllPath_Test2()
    {
        Edge<String, String> edge = new Edge<string, string>()
            { Destination = "B", Source = "A", EdgeContent = "AB", weight = 100 };
        Edge<String, String> edge2 = new Edge<string, string>()
            { Destination = "C", Source = "A", EdgeContent = "AC", weight = 100 };
        Edge<String, String> edge3 = new Edge<string, string>()
            { Destination = "D", Source = "C", EdgeContent = "BD", weight = 100 };
        Edge<String, String> edge4 = new Edge<string, string>()
            { Destination = "E", Source = "D", EdgeContent = "BE", weight = 100 };

        CustomGraph<String, String> graph = new CustomGraph<string, string>();
        graph.AddEdge(edge);
        graph.AddEdge(edge2);
        graph.AddEdge(edge3);
        graph.AddEdge(edge4);

        GraphProcessor<String, String> graphProcessor = new GraphProcessor<string, string>();
        graphProcessor._graph = graph;
        var path = graphProcessor.GetMaxFlow("A", "D");
        Assert.Equal(100, path);
    }

    public void RealWorldTest()
    {
        
    }
}