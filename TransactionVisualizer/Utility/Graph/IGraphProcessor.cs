using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Models.Graph.Graph;

public interface IGraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination);

    void LenghtExpand(int maxLenght, Stack<TVertex> vertices, IModelRepository<BusinessModels.Transaction.Transaction> edgesRepository);

    decimal GetMaxFlow(TVertex source, TVertex destination);
    
    void SetGraph(Graph<TVertex, TEdge> graph);
    
    Graph<TVertex, TEdge> GetGraph();
}