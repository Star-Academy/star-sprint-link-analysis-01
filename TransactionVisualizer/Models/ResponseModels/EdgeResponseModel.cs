namespace TransactionVisualizer.Models.ResponseModels;

public class EdgeResponseModel<TEdge> where TEdge : class
{
    public long Source { get; init; }
    public long Destination { get; init; }
    public TEdge Content { get; init; }
}
