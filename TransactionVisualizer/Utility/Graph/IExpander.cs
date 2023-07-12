using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Graph;

public interface IExpander<TVertex, TEdge> where TVertex : class where TEdge : class
{
    Graph<TVertex, TEdge> Expand(int maxLenght, TVertex vertex, Graph<TVertex, TEdge> graph);
}