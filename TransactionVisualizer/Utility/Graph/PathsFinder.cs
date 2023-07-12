using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Utility.Graph;

public class PathsFinder<TVertex, TEdge> : IPathsFinder<TVertex, TEdge> where TEdge : class where TVertex : class
{
    public List<List<Edge<TVertex, TEdge>>> Find(TVertex source, TVertex destination, Graph<TVertex, TEdge> graph)
    {
        var paths = new List<List<Edge<TVertex, TEdge>>>();

        var queue = new Queue<List<Edge<TVertex, TEdge>>>();
        queue.Enqueue(new List<Edge<TVertex, TEdge>>());

        BreadthFirstSearch(source, destination, graph, queue, paths);

        return paths;
    }

    private static void BreadthFirstSearch(
        TVertex source,
        TVertex destination,
        Graph<TVertex, TEdge> graph,
        Queue<List<Edge<TVertex, TEdge>>> queue,
        ICollection<List<Edge<TVertex, TEdge>>> paths
    )
    {
        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentVertex = currentPath.Count > 0 ? currentPath.Last().Destination : source;

            if (currentVertex.Equals(destination)) paths.Add(new List<Edge<TVertex, TEdge>>(currentPath));

            if (!graph.AdjacencyMatrix.TryGetValue(currentVertex, out var edges)) continue;

            foreach (var newPath in from edge in edges
                     let nextVertex = edge.Destination
                     where !currentPath.Select(e => e.Destination).Contains(nextVertex)
                     select new List<Edge<TVertex, TEdge>>(currentPath)
                     {
                         edge
                     }
                    )
                queue.Enqueue(newPath);
        }
    }
}