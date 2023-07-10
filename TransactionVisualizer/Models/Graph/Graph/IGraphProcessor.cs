namespace TransactionVisualizer.Models.Graph.Graph;

public interface IGraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination);
    void LenghtExpand(int maxLenght , Stack<TVertex> vertices , List<Edge<TVertex , TEdge>> edges);
    decimal GetMaxFlow(TVertex source, TVertex destination);
    void SetGraph(CustomGraph<TVertex , TEdge> graph);
    CustomGraph<TVertex , TEdge> GetGraph();

}