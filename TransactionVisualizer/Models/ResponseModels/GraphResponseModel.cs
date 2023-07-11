namespace TransactionVisualizer.Models.ResponseModels;

public class GraphResponseModel<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public List<TVertex> Vertices { get; set; }
    public List<EdgeResponseModel<TEdge>> Edges { get; init; }
    public long VertexCount { get; set; }
    public long EdgeCount { get; set; }
}