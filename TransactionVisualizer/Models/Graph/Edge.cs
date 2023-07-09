namespace TransactionVisualizer.Models.Graph;

public class Edge<T, U> where T : class where U : class
{
    public U EdgeContent { get; set; }

    public T Destination { get; set; }
    public decimal weight { get; set; }
    public T Source { get; set; }
}