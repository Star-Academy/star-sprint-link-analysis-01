using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Utility.Graph;

public class GraphProcessor<TVertex, TEdge> where TVertex : class where TEdge : class
{
    CustomGraph<TVertex, TEdge> _graph;

    public GraphProcessor(CustomGraph<TVertex, TEdge> graph)
    {
        _graph = graph;
    }

    public List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination)
    {
        List<List<Edge<TVertex, TEdge>>> allPaths = new List<List<Edge<TVertex, TEdge>>>();

        Queue<List<Edge<TVertex, TEdge>>> queue = new Queue<List<Edge<TVertex, TEdge>>>();
        queue.Enqueue(new List<Edge<TVertex, TEdge>>());

        while (queue.Count > 0)
        {
            List<Edge<TVertex, TEdge>> currentPath = queue.Dequeue();
            TVertex currentVertex = currentPath.Count > 0 ? currentPath.Last().Destination : source;

            if (currentVertex.Equals(destination))
            {
                // Found a path from source to destination
                allPaths.Add(new List<Edge<TVertex, TEdge>>(currentPath));
            }

            if (_graph.adjacencyMatrix.TryGetValue(currentVertex, out var edges))
            {
                foreach (var edge in edges)
                {
                    TVertex nextVertex = edge.Destination;
                    if (!currentPath.Select(e => e.Destination).Contains(nextVertex))
                    {
                        List<Edge<TVertex, TEdge>> newPath = new List<Edge<TVertex, TEdge>>(currentPath);
                        newPath.Add(edge);
                        queue.Enqueue(newPath);
                    }
                }
            }
        }

        return allPaths;
    }

    public void LenghtExpand(int maxLenght , Stack<TVertex> vertices , List<Edge<TVertex , TEdge>> edges)
    {
        if (maxLenght == 0 || vertices.Count == 0)
        {
            return;
        }
        int nextLenght = maxLenght - 1;
        TVertex currentVertex = vertices.Pop();
        edges.ForEach(item =>
        {
            _graph.AddEdge(item);
            vertices.Push(item.Destination);
        });
        LenghtExpand(nextLenght , vertices , edges);
    }

    public int GetMaxFlow(TVertex source, TVertex destination)
    {
        var allPath = GetAllPaths(source, destination);
        int maxFlow = 0;
        foreach (var path in allPath)
        {
            int min = int.MaxValue;
            foreach (var edge in path)
            {
                min = Math.Min(edge.weight, min);
            }

            maxFlow += min;
        }

        return maxFlow;
    }
}