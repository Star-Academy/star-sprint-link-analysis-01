namespace TransactionVisualizer.Models.ResponseModels;

public class GraphResponseModel<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public long VertexCount { get; init; }
    public long EdgeCount { get; init; }
    public List<TVertex> Vertices { get; init; }
    public List<EdgeResponseModel<TEdge>> Edges { get; init; }
}