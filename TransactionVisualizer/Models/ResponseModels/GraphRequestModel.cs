using TransactionVisualizer.Models.BusinessModels;

namespace TransactionVisualizer.Models.ResponseModels;

public class GraphResponseModel<TVertex, TEdge> where TVertex : BaseModel where TEdge : class
{
    public List<TVertex> Vertices { set; get; }
    public List<EdgeResponseModel<TEdge>> Edges { set; get; }
}