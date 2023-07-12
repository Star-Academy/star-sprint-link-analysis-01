namespace TransactionVisualizer.Models.DataStructureModels.Graph.Graph;

public interface IGraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination);
    
    void LenghtExpand(int maxLenght , Stack<TVertex> vertices , List<Edge<TVertex , TEdge>> edges);
    
    decimal GetMaxFlow(TVertex source, TVertex destination);
    
    void SetGraph(Graph<TVertex , TEdge> graph);
    
    Graph<TVertex , TEdge> GetGraph();
}