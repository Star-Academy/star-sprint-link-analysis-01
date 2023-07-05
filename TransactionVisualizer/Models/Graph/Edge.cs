namespace TransactionVisualizer.Models.Graph;

public class Edge<T, U> where T : class where U : class
{
    public U Content { get; set; }
    public T Source { get; set; }
    public T Destination { get; set; }
    
}