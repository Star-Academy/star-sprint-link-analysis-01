namespace TransactionVisualizer.Models.Graph;

public class EdgeConfig<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public TEdge Content { get; set; }

    public TVertex Source { get; set; }
    public TVertex Destination { get; set; }
    public decimal Weight { get; set; }
}