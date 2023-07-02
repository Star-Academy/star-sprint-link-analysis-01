namespace TransactionVisualizer.Models.Graph;

public class Edge<T, U>
{
    public T Content { get; set; }
    public U Source { get; set; }
    public U Destination { get; set; }

    public Edge(T content, U source, U destination)
    {
        Content = content;
        Source = source;
        Destination = destination;
    }
}