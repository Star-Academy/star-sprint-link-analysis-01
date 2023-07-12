namespace TransactionVisualizer.Models.DataStructureModels.Graph.Graph;

public class GraphProcessor<TVertex, TEdge> : IGraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    private Graph<TVertex, TEdge> Graph { get; set; }

    public List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination)
    {
        var allPaths = new List<List<Edge<TVertex, TEdge>>>();
        var queue = new Queue<List<Edge<TVertex, TEdge>>>();
        queue.Enqueue(new List<Edge<TVertex, TEdge>>());


        BreadthFirstSearch(source, destination, queue, allPaths);

        return allPaths;
    }

    private void BreadthFirstSearch(TVertex source, TVertex destination, Queue<List<Edge<TVertex, TEdge>>> queue,
        ICollection<List<Edge<TVertex, TEdge>>> allPaths)
    {
        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentVertex = currentPath.Count > 0 ? currentPath.Last().Destination : source;

            if (currentVertex.Equals(destination))
            {
                allPaths.Add(new List<Edge<TVertex, TEdge>>(currentPath));
            }

            if (Graph.AdjacencyMatrix.TryGetValue(currentVertex, out var edges))
            {
                foreach (var edge in edges)
                {
                    var nextVertex = edge.Destination;
                    if (!currentPath.Select(e => e.Destination).Contains(nextVertex))
                    {
                        var newPath = new List<Edge<TVertex, TEdge>>(currentPath) { edge };

                        queue.Enqueue(newPath);
                    }
                }
            }
        }
    }

    public void LenghtExpand(int maxLenght, Stack<TVertex> vertices, List<Edge<TVertex, TEdge>> edges)
    {
        if (maxLenght == 0 || vertices.Count == 0)
        {
            return;
        }
    
        var nextLenght = maxLenght - 1;
        var currentVertex = vertices.Pop();
    
        edges = edges.Where(item => item.Source.Equals(currentVertex)).ToList();
        edges.ForEach
        (
            item =>
            {
                Graph.AddEdge(item);
                vertices.Push(item.Destination);
            }
        );
    
        LenghtExpand(nextLenght, vertices, edges);
    }

    public decimal GetMaxFlow(TVertex source, TVertex destination)
    {
        var allPath = GetAllPaths(source, destination);
        decimal maxFlow = 0;

        var min = decimal.MaxValue;
        foreach (var path in allPath)
        {
            min = path.Select(edge => edge.Weight).Prepend(min).Min();

            maxFlow += min;
        }

        return maxFlow;
    }

    public void SetGraph(Graph<TVertex, TEdge> graph)
    {
        Graph = graph;
    }

    public Graph<TVertex, TEdge> GetGraph()
    {
        return Graph;
    }
}