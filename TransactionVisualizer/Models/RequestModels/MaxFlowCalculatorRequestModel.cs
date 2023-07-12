using TransactionVisualizer.Models.ResponseModels;

namespace TransactionVisualizer.Models.RequestModels;

public class MaxFlowCalculatorRequestModel<TVertex, TEdge> where TEdge : class where TVertex : class
{
    public GraphResponseModel<TVertex, TEdge> CurrentState { get; set; }
    public TVertex Source { get; set; }
    public TVertex Destenation { get; set; }
}