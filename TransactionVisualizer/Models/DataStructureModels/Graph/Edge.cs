namespace TransactionVisualizer.Models.DataStructureModels.Graph;

public class Edge<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public TVertex Source { get; init; }
    public TVertex Destination { get; init; }
    public TEdge Content { get; init; }
    public decimal Weight { get; init; }
}