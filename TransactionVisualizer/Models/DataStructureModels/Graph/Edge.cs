namespace TransactionVisualizer.Models.DataStructureModels.Graph;

public class Edge<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public TVertex Source { get; set; }
    public TVertex Destination { get; set; }
    public TEdge Content { get; set; }
    public decimal Weight { get; init; }
}