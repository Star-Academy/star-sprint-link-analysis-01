using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;

public interface IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> config);
}