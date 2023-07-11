namespace TransactionVisualizer.Models.DataStructureModels.Graph;

public class Graph<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public Graph()
    {
        AdjacencyMatrix = new Dictionary<TVertex, List<Edge<TVertex, TEdge>>>();
    }

    public Dictionary<TVertex, List<Edge<TVertex, TEdge>>> AdjacencyMatrix { get; }

    public void AddEdge(Edge<TVertex, TEdge> edge)
    {
        if (AdjacencyMatrix.TryGetValue(edge.Source, out var value))
        {
            if (!value.Contains(edge)) value.Add(edge);
        }
        else
        {
            AdjacencyMatrix.Add(edge.Source, new List<Edge<TVertex, TEdge>>());
            AdjacencyMatrix[edge.Source].Add(edge);
        }

        if (!AdjacencyMatrix.ContainsKey(edge.Destination))
            AdjacencyMatrix.Add(edge.Destination, new List<Edge<TVertex, TEdge>>());
    }
}