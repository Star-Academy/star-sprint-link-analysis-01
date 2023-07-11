using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Graph;

public class MaxFlowCalculator<TVertex, TEdge> : IMaxFlowCalculator<TVertex, TEdge>
    where TEdge : class where TVertex : class
{
    private readonly IPathsFinder<TVertex, TEdge> _pathsFinder;

    public MaxFlowCalculator(IPathsFinder<TVertex, TEdge> pathsFinder)
    {
        _pathsFinder = pathsFinder;
    }

    public decimal Calculate(TVertex source, TVertex destination, Graph<TVertex, TEdge> graph)
    {
        var paths = _pathsFinder.Find(source, destination, graph);

        return paths.Sum(path => path.Select(edge => edge.Weight).Prepend(decimal.MaxValue).Min());
    }
}