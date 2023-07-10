namespace TransactionVisualizer.Models.ResponseModels;

public class EdgeResponseModel<TEdge> where TEdge : class
{
    public TEdge Content { get; set; }
    public long Destination { get; set; }
    public long Source { get; set; }
    
}