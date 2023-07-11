using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Converters;

namespace TransactionVisualizer.Utility.Graph;

public class GraphProcessor<TVertex, TEdge> : IGraphProcessor<TVertex, TEdge> where TEdge : class where TVertex : class
{
    private readonly IModelToGraphEdge<Transaction, TVertex, TEdge> _modelToGraphEdge;

    public GraphProcessor(IModelToGraphEdge<Transaction, TVertex, TEdge> modelToGraphEdge)
    {
        Graph = new Graph<TVertex, TEdge>();
        _modelToGraphEdge = modelToGraphEdge;
    }

    public Graph<TVertex, TEdge> Graph { set; get; }


    public List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination)
    {
        var allPaths = new List<List<Edge<TVertex, TEdge>>>();
        var queue = new Queue<List<Edge<TVertex, TEdge>>>();
        queue.Enqueue(new List<Edge<TVertex, TEdge>>());


        BFS(source, destination, queue, allPaths);

        return allPaths;
    }

    public void LenghtExpand(int maxLenght, Stack<TVertex> vertices, IDataRepository<Transaction> edgesRepository)
    {
        if (maxLenght == 0 || vertices.Count == 0) return;

        var nextLenght = maxLenght - 1;
        var currentVertex = vertices.Pop();
        Console.WriteLine(currentVertex.ToString());

        var edges = edgesRepository.Search(descriptor
            => descriptor.Query(containerDescriptor
                => containerDescriptor.Match(
                    queryDescriptor =>
                        queryDescriptor.Field("sourceAccount").Query(currentVertex.ToString())
                )
            )
        );

        edges.Items.ForEach(item =>
        {
            Console.WriteLine(item.SourceAccount + " -> " + item.DestinationAccount);
            vertices.Push(_modelToGraphEdge.Convert(item).Destination);
            Graph.AddEdge(_modelToGraphEdge.Convert(item));
        });

        LenghtExpand(nextLenght, vertices, edgesRepository);
    }

    public decimal GetMaxFlow(TVertex source, TVertex destination)
    {
        var allPath = GetAllPaths(source, destination);
        decimal maxFlow = 0;

        allPath.ForEach(item =>
        {
            item.ForEach(path =>
            {
                Console.Write(path.Source + " -> " + path.Destination + " : " + path.Weight + "   ,  ");
            });
            Console.WriteLine();
        });

        foreach (var path in allPath)
        {
            var min = decimal.MaxValue;
            foreach (var edge in path) min = Math.Min(edge.Weight, min);

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


    private void BFS(TVertex source, TVertex destination, Queue<List<Edge<TVertex, TEdge>>> queue,
        List<List<Edge<TVertex, TEdge>>> allPaths)
    {
        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentVertex = currentPath.Count > 0 ? currentPath.Last().Destination : source;

            if (currentVertex.Equals(destination))
                // Found a path from source to destination
                allPaths.Add(new List<Edge<TVertex, TEdge>>(currentPath));

            if (Graph.AdjacencyMatrix.TryGetValue(currentVertex, out var edges))
                foreach (var edge in edges)
                {
                    var nextVertex = edge.Destination;
                    if (!currentPath.Select(e => e.Destination).Contains(nextVertex))
                    {
                        var newPath = new List<Edge<TVertex, TEdge>>(currentPath);
                        newPath.Add(edge);
                        queue.Enqueue(newPath);
                    }
                }
        }
    }
}