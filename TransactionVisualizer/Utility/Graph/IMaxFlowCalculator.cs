using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Graph;

public interface IMaxFlowCalculator<TVertex, TEdge> where TVertex : class where TEdge : class
{
    decimal Calculate(TVertex source, TVertex destination, Graph<TVertex, TEdge> graph);
}