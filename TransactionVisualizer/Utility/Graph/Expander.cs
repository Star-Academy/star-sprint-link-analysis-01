using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;

namespace TransactionVisualizer.Utility.Graph;

public class Expander<TVertex, TEdge> : IExpander<TVertex, TEdge> where TEdge : class where TVertex : class
{
    private readonly IModelToGraphEdge<TEdge, TVertex, TEdge> _modelToGraphEdge;
    private readonly IDataRepository<TEdge> _repository;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;

    public Expander(IDataRepository<TEdge> repository, ISelectorBuilder selectorBuilder,
        IModelToGraphEdge<TEdge, TVertex, TEdge> modelToGraphEdge, ISelectorKeyValueBuilder selectorKeyValueBuilder)
    {
        _repository = repository;
        _selectorBuilder = selectorBuilder;
        _modelToGraphEdge = modelToGraphEdge;
        _selectorKeyValueBuilder = selectorKeyValueBuilder;
    }

    public Graph<TVertex, TEdge> Expand(int maxLenght, TVertex vertex, Graph<TVertex, TEdge> graph)
    {
        var index = maxLenght;
        var stack = new Stack<TVertex>();
        stack.Push(vertex);
        while (index >= 0 && stack.Count > 0)
        {
            index--;
            var currentVertex = stack.Pop();
            Console.WriteLine(currentVertex.ToString());

            var edges = _repository.Search(
                _selectorBuilder.BuildKeyValueSelector<TEdge>(
                    _selectorKeyValueBuilder.BuildFindTransactionBySourceAccount(
                        currentVertex.ToString()
                    )
                )
            );

            edges.Items.ForEach(
                item =>
                {
                    stack.Push(_modelToGraphEdge.Convert(item).Destination);
                    graph.AddEdge(_modelToGraphEdge.Convert(item));
                }
            );
        }

        return graph;
    }
}