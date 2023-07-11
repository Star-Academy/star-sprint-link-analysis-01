using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.Models.BusinessModels;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph.Graph;
using TransactionVisualizer.Utility.Converters;

namespace TransactionVisualizer.Utility.Graph;

public class GraphProcessor<TVertex, TEdge> : IGraphProcessor<TVertex, TEdge> where TVertex : BaseModel where TEdge : class
{
    private IModelToGraphEdge<Transaction, TVertex, TEdge> _modelToGraphEdge;

    public Graph<TVertex, TEdge> _graph { set; get; }

    public GraphProcessor(IModelToGraphEdge<Transaction, TVertex, TEdge> modelToGraphEdge)
    {
        _graph = new Graph<TVertex, TEdge>();
        _modelToGraphEdge = modelToGraphEdge;
    }


    public List<List<Edge<TVertex, TEdge>>> GetAllPaths(TVertex source, TVertex destination)
    {
        List<List<Edge<TVertex, TEdge>>> allPaths = new List<List<Edge<TVertex, TEdge>>>();
        Queue<List<Edge<TVertex, TEdge>>> queue = new Queue<List<Edge<TVertex, TEdge>>>();
        queue.Enqueue(new List<Edge<TVertex, TEdge>>());


        BFS(source, destination, queue, allPaths);

        return allPaths;
    }


    private void BFS(TVertex source, TVertex destination, Queue<List<Edge<TVertex, TEdge>>> queue,
        List<List<Edge<TVertex, TEdge>>> allPaths)
    {
        while (queue.Count > 0)
        {
            List<Edge<TVertex, TEdge>> currentPath = queue.Dequeue();
            TVertex currentVertex = currentPath.Count > 0 ? currentPath.Last().Destination : source;

            if (currentVertex.Equals(destination))
            {
                // Found a path from source to destination
                allPaths.Add(new List<Edge<TVertex, TEdge>>(currentPath));
            }

            if (_graph.AdjacencyMatrix.TryGetValue(currentVertex, out var edges))
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
    }

    public void LenghtExpand(int maxLenght, Stack<TVertex> vertices, IModelRepository<Transaction> edgesRepository)
    {
        if (maxLenght == 0 || vertices.Count == 0)
        {
            return;
        }

        int nextLenght = maxLenght - 1;
        TVertex currentVertex = vertices.Pop();
        Console.WriteLine(currentVertex.ToString());
        
        var edges = edgesRepository.Search(descriptor
            => descriptor.Query(containerDescriptor
                => containerDescriptor.Match(
                    queryDescriptor =>
                        queryDescriptor.Field("sourceAccount").Query(currentVertex.ToString())
                )
            )
        );

        edges.ForEach(item =>
        {
            Console.WriteLine(item.SourceAccount + " -> " + item.DestinationAccount);
            vertices.Push(_modelToGraphEdge.Convert(item).Destination);
            _graph.AddEdge(_modelToGraphEdge.Convert(item));
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
            decimal min = decimal.MaxValue;
            foreach (var edge in path)
            {
                min = Math.Min(edge.Weight, min);
            }

            maxFlow += min;
        }

        return maxFlow;
    }

    public void SetGraph(Graph<TVertex, TEdge> graph)
    {
        _graph = graph;
    }

    public Graph<TVertex, TEdge> GetGraph()
    {
        return _graph;
    }
}