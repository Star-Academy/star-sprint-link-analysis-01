using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Graph;

public interface IPathsFinder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    List<List<Edge<TVertex, TEdge>>> Find(TVertex source, TVertex destination, Graph<TVertex, TEdge> graph);
}