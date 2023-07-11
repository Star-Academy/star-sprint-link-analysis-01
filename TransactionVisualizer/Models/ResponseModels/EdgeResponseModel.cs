namespace TransactionVisualizer.Models.ResponseModels;

public class EdgeResponseModel<TEdge> where TEdge : class
{
    public long Destination { get; init; }
    public long Source { get; init; }
    public TEdge Content { get; init; }
}