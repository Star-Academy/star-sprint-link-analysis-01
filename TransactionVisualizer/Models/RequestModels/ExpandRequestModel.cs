using TransactionVisualizer.Models.BusinessModels;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ResponseModels;

namespace TransactionVisualizer.Models.RequestModels;

public class ExpandRequestModel<TVertex, TEdge> where TVertex :  class where TEdge : class
{
    public GraphResponseModel<TVertex, TEdge> CurrentState { get; set; }
    public int MaxLength { get; set; }
    public TVertex Vertex { get; set; }
}