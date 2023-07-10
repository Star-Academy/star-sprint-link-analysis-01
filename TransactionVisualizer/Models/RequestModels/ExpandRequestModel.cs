using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Models.RequestModels;

public class ExpandRequestModel<TVertex, TEdge> where TVertex :  class where TEdge : class
{
    public CustomGraph<TVertex, TEdge> CurrentState { get; set; }
    public int MaxLength { get; set; }
    public TVertex Vertex { get; set; }
}