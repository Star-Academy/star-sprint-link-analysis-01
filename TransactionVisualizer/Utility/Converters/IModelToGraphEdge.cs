using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Utility.Converters;

public interface IModelToGraphEdge<in TModel, TVertex, TEdge> where TEdge : class where TVertex : class
{
    public Edge<TVertex, TEdge> Convert(TModel transaction);
}