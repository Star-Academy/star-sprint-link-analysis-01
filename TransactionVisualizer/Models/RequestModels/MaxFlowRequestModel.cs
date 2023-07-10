using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Models.ResponseModels;

public class MaxFlowRequestModel<TVertex, TEdge> where TEdge : class where TVertex : class
{
    public CustomGraph<TVertex, TEdge> CurrentState { get; set; }
    public TVertex Source { get; set; }
    public TVertex Destenation { get; set; }
}