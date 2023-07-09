using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Utility.Builder;

public interface IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> config);
}