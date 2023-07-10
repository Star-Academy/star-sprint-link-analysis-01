namespace TransactionVisualizer.Models.DataStructureModels.Graph.Builder;

public interface IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> config);
}