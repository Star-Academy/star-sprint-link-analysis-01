using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Converters;

public interface IModelToGraphEdge<in TModel, TVertex, TEdge> where TEdge : class where TVertex : class
{
    public Edge<TVertex, TEdge> Convert(TModel transaction);
}