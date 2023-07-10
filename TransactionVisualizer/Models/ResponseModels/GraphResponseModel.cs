using TransactionVisualizer.Models.BusinessModels;

namespace TransactionVisualizer.Models.ResponseModels;

public class GraphResponseModel<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public List<TVertex> Vertices { set; get; }
    public List<EdgeResponseModel<TEdge>> Edges { set; get; }
    public long VertexCount { set; get; }
    public long EdgeCount { get; set; }
}