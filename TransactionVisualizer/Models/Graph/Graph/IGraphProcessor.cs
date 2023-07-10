using TransactionVisualizer.DataRepository.ModelsRepository;

namespace TransactionVisualizer.Models.Graph.Graph;

public interface IGraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination);
    void LenghtExpand(int maxLenght, Stack<TVertex> vertices, IModelRepository<Edge<TVertex, TEdge>> edgesRepository);
    decimal GetMaxFlow(TVertex source, TVertex destination);
    void SetGraph(CustomGraph<TVertex, TEdge> graph);
    CustomGraph<TVertex, TEdge> GetGraph();
}