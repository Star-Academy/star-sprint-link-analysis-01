using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;

using Validator;

public class EdgeBuilder<TVertex, TEdge> : IEdgeBuilder<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public Edge<TVertex, TEdge> Build(EdgeConfig<TVertex, TEdge> config)
    {
        Validator.NullValidation(config);
        
        return new Edge<TVertex, TEdge>
        {
            Source = config.Source,
            Destination = config.Destination,
            Content = config.Content,
            Weight = config.Weight
        };
    }
}