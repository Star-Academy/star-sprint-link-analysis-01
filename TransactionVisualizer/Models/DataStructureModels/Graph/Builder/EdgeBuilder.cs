namespace TransactionVisualizer.Models.DataStructureModels.Graph.Builder;

public class EdgeBuilder<TVertex, TEdge> : IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> edgeConfig)
    {
        return new Edge<TVertex, TEdge>()
        {
            Source = edgeConfig.Source,
            Destination = edgeConfig.Destination,
            Content = edgeConfig.Content,
            Weight = edgeConfig.Weight
        };
    }
}