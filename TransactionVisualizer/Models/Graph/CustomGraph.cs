namespace TransactionVisualizer.Models.Graph;

public class CustomGraph<U , T> 
{
    public List<U> Vertex { get; set; }
    public List<Edge<T, U>> Edge { get; set; }

    public CustomGraph()
    {
        Vertex = new List<U>();
        Edge = new List<Edge<T, U>>();
    }
    public void AddVertex(U vertex)
    {
        Vertex.Add(vertex);
    }
   
    public void AddEdge(T content , U source, U destination)
    {
        Edge.Add(new Edge<T ,U>(content , source , destination));
    }
}