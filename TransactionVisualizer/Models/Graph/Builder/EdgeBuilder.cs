using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Utility.Builder;

public class EdgeBuilder<TVertex, TEdge> : IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> edgeConfig)
    {
        return new Edge<TVertex, TEdge>()
            { Content = edgeConfig.Content, Destination = edgeConfig.Destination, Source = edgeConfig.Source };
    }
}