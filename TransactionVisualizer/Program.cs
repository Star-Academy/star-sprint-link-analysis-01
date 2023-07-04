using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility;

public class Program
{
    public static void Main(string[] args)
    {
        Transaction transaction_0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction_1 = new Transaction
        {
            ID = 1,
            SourceAcount = 1,
            DestiantionAccount = 2,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction_2 = new Transaction
        {
            ID = 2,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction_3 = new Transaction
        {
            ID = 3,
            SourceAcount = 2,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };

        List<long> acc = new List<long>();
        acc.Add(0);
        acc.Add(1);
        acc.Add(2);
        
        List<Transaction> trs = new List<Transaction>();
        trs.Add(transaction_0);
        trs.Add(transaction_1);
        trs.Add(transaction_2);
        trs.Add(transaction_3);
        
        TransactionsNetworkGraphCreator transactionsNetworkGraphCreator = new TransactionsNetworkGraphCreator(
            acc,
            trs
        );

        Graph<long, Transaction> graph = transactionsNetworkGraphCreator.CreateTransactionsNetworkGraph();

        List<List<Edge<long, Transaction>>> pat = graph.FindPaths(0, 2, 2000);
        
        foreach (List<Edge<long,Transaction>> edges in pat)
        {
            foreach (Edge<long,Transaction> edge in edges)
            {
                Console.Write(edge.Information.ID + " ");
            }

            Console.WriteLine();
        }

    }
}

public class TransactionsNetworkGraphCreator
{
    private readonly List<long> _accounts;
    private readonly List<Transaction> _transactions;

    public TransactionsNetworkGraphCreator(List<long> accounts, List<Transaction> transactions)
    {
        _accounts = accounts;
        _transactions = transactions;
    }

    public Graph<long, Transaction> CreateTransactionsNetworkGraph()
    {
        List<Edge<long, Transaction>> edges = new List<Edge<long, Transaction>>();
        foreach (Transaction edge in _transactions)
        {
            edges.Add(new Edge<long, Transaction>(edge.SourceAcount, edge.DestiantionAccount, edge, edge.Amount));
        }
        return new GraphCreator<long, Transaction>(_accounts, edges).CreateGraph();
    }
}

public class GraphCreator<T, U>
{
    private List<T> Vertexes { get; set; }
    private List<Edge<T, U>> Edges { get; set; }

    public GraphCreator(List<T> vertexes, List<Edge<T, U>> edges)
    {
        Vertexes = vertexes;
        Edges = edges;
    }

    public Graph<T, U> CreateGraph()
    {
        Graph<T, U> graph = new Graph<T, U>();

        foreach (T vertex in Vertexes)
        {
            graph.AddVertex(vertex);
        }

        foreach (Edge<T, U> edge in Edges)
        {
            graph.AddEdge(edge.Source, edge.Destination, edge.Information, edge.Weight);
        }

        return graph;
    }
}

public class Edge<T, U>
{
    public T Source { get; }
    public T Destination { get; }
    public U Information { get; }
    public decimal Weight { get; set; }

    public Edge(T source, T destination, U information, decimal weight)
    {
        Source = source;
        Destination = destination;
        Information = information;
        Weight = weight;
    }
}

public class Graph<T, U>
{
    private readonly Dictionary<T, List<Edge<T, U>>> _adjacencyList;

    public Graph()
    {
        _adjacencyList = new Dictionary<T, List<Edge<T, U>>>();
    }

    public void AddVertex(T vertex)
    {
        _adjacencyList[vertex] = new List<Edge<T, U>>();
    }

    public void AddEdge(T source, T destination, U information, decimal weight)
    {
        _adjacencyList[source].Add(new Edge<T, U>(source, destination, information, weight));
    }

    public List<List<Edge<T, U>>> FindPaths(T startVertex, T endVertex, int weightOfPaths)
    {
        List<List<Edge<T, U>>> paths = new List<List<Edge<T, U>>>();
        List<Edge<T, U>> currentPath = new List<Edge<T, U>>();


        DepthFirstSearch(startVertex, endVertex, weightOfPaths, 0, currentPath, paths);

        return paths;
    }

    private void DepthFirstSearch(
        T currentVertex,
        T endVertex,
        decimal weightOfPaths,
        decimal currentWeightOfPaths,
        List<Edge<T, U>> currentPath,
        List<List<Edge<T, U>>> paths
    )
    {
        if (currentVertex.Equals(endVertex) && currentWeightOfPaths == weightOfPaths)
        {
            paths.Add(new List<Edge<T, U>>(currentPath));

            currentPath.Clear();
        }
        else
        {
            foreach (Edge<T, U> edge in _adjacencyList[currentVertex])
            {
                currentPath.Add(edge);

                DepthFirstSearch(edge.Destination,
                    endVertex,
                    weightOfPaths,
                    currentWeightOfPaths + edge.Weight,
                    currentPath,
                    paths
                );
            }
        }
    }
}


// using TransactionVisualizer.Models;
// using TransactionVisualizer.Models.Transaction;
//
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         List<Account> _accounts;
//
//         Branch branch = new Branch()
//         {
//             Id = 0,
//             Address = "0",
//             Name = "0",
//             Telephone = "0"
//         };
//         Owner owner = new Owner()
//         {
//             ID = 0,
//             Name = "0",
//             FamilyName = "0",
//         };
//         _accounts = new List<Account>()
//         {
//             new Account()
//             {
//                 AccountID = 0,
//                 CardID = "0",
//                 Sheba = "0",
//                 AccountType = AccountType.Jari,
//                 Branch = branch,
//                 Owner = owner,
//             },
//             new Account()
//             {
//                 AccountID = 1,
//                 CardID = "1",
//                 Sheba = "1",
//                 AccountType = AccountType.Jari,
//                 Branch = branch,
//                 Owner = owner,
//             },
//             new Account()
//             {
//                 AccountID = 2,
//                 CardID = "2",
//                 Sheba = "2",
//                 AccountType = AccountType.Jari,
//                 Branch = branch,
//                 Owner = owner,
//             },
//             new Account()
//             {
//                 AccountID = 3,
//                 CardID = "3",
//                 Sheba = "3",
//                 AccountType = AccountType.Jari,
//                 Branch = branch,
//                 Owner = owner,
//             },
//         };
//
//         GraphPrintTransaction<Account, Transaction> graphPrintTransaction = new GraphPrintTransaction<Account, Transaction>();
//         Vertex<Account> account_0 = new Vertex<Account>(_accounts[0]);
//         Vertex<Account> account_1 = new Vertex<Account>(_accounts[1]);
//         Vertex<Account> account_2 = new Vertex<Account>(_accounts[2]);
//         graphPrintTransaction.AddVertex(account_0);
//         graphPrintTransaction.AddVertex(account_1);
//         graphPrintTransaction.AddVertex(account_2);
//         Transaction transaction_0 = new Transaction
//         {
//             ID = 0,
//             SourceAcount = 0,
//             DestiantionAccount = 1,
//             TransactionType = TransactionType.Paya,
//             Amount = 1000,
//             Date = DateTime.Now,
//         };
//         Transaction transaction_1 = new Transaction
//         {
//             ID = 1,
//             SourceAcount = 1,
//             DestiantionAccount = 2,
//             TransactionType = TransactionType.Paya,
//             Amount = 1000,
//             Date = DateTime.Now,
//         };
//         Transaction transaction_2 = new Transaction
//         {
//             ID = 2,
//             SourceAcount = 0,
//             DestiantionAccount = 1,
//             TransactionType = TransactionType.Paya,
//             Amount = 1000,
//             Date = DateTime.Now,
//         };
//         Transaction transaction_3 = new Transaction
//         {
//             ID = 3,
//             SourceAcount = 2,
//             DestiantionAccount = 1,
//             TransactionType = TransactionType.Paya,
//             Amount = 1000,
//             Date = DateTime.Now,
//         };
//         Transaction transaction_4 = new Transaction
//         {
//             ID = 4,
//             SourceAcount = 0,
//             DestiantionAccount = 1,
//             TransactionType = TransactionType.Paya,
//             Amount = 10000,
//             Date = DateTime.Now,
//         };
//         Transaction transaction_5 = new Transaction
//         {
//             ID = 5,
//             SourceAcount = 1,
//             DestiantionAccount = 2,
//             TransactionType = TransactionType.Paya,
//             Amount = 10000,
//             Date = DateTime.Now,
//         };
//         graphPrintTransaction.AddEdge(account_0, account_1, transaction_0, 1000);
//         graphPrintTransaction.AddEdge(account_1, account_2, transaction_1, 1000);
//         graphPrintTransaction.AddEdge(account_0, account_1, transaction_2, 1000);
//         graphPrintTransaction.AddEdge(account_2, account_1, transaction_3, 1000);
//         // graph.AddEdge(account_0, account_1, transaction_4, 10000);
//         // graph.AddEdge(account_1, account_2, transaction_5, 10000);
//
//         List<List<Edge<Account, Transaction>>> paths = graphPrintTransaction.FindPaths(account_0, account_2, 2000);
//         foreach (List<Edge<Account, Transaction>> path in paths)
//         {
//             foreach (Edge<Account, Transaction> vertex in path)
//             {
//                 Console.Write(vertex.Information.ID + " ");
//             }
//
//             Console.WriteLine();
//         }
//     }
// }
//
// public class Vertex<T>
// {
//     public T Data { get; }
//
//     public Vertex(T data)
//     {
//         Data = data;
//     }
// }
//
// public class Edge<T, U>
// {
//     public Vertex<T> Source { get; }
//     public Vertex<T> Destination { get; }
//     public U Information { get; }
//     public long Weight { get; }
//
//     public Edge(Vertex<T> source, Vertex<T> destination, U information, long weight)
//     {
//         Source = source;
//         Destination = destination;
//         Information = information;
//         Weight = weight;
//     }
// }
//
// public class GraphPrintTransactionAndAccount<T, U>
// {
//     private Dictionary<Vertex<T>, List<Edge<T, U>>> adjacencyList;
//
//     public GraphPrintTransactionAndAccount()
//     {
//         adjacencyList = new Dictionary<Vertex<T>, List<Edge<T, U>>>();
//     }
//
//     public void AddVertex(Vertex<T> vertex)
//     {
//         adjacencyList[vertex] = new List<Edge<T, U>>();
//     }
//
//     public void AddEdge(Vertex<T> source, Vertex<T> destination, U information, long weight)
//     {
//         Edge<T, U> edge = new Edge<T, U>(source, destination, information, weight);
//         adjacencyList[source].Add(edge);
//     }
//
//     public List<List<Vertex<T>>> FindPaths(Vertex<T> startVertex, Vertex<T> endVertex, int numEdges)
//     {
//         List<List<Vertex<T>>> paths = new List<List<Vertex<T>>>();
//         List<Vertex<T>> currentPath = new List<Vertex<T>>();
//         
//         List<List<Edge<T, U>>> edgess = new List<List<Edge<T, U>>>();
//         List<Edge<T, U>> currentEdgeee = new List<Edge<T, U>>();
//
//
//
//         currentPath.Add(startVertex);
//         DFS(startVertex, endVertex, numEdges, 0, currentPath, paths, currentEdgeee, edgess);
//         
//         
//         foreach (List<Edge<T, U>> path in edgess)
//         {
//             foreach (Edge<T, U> vertex in path)
//             {
//                 Console.Write(vertex.Information + " ");
//             }
//
//             Console.WriteLine();
//         }
//
//         Console.WriteLine("------------------------------");
//         return paths;
//     }
//
//     private void DFS(Vertex<T> currentVertex, Vertex<T> endVertex, long totalWeight, long currentWeight,
//         List<Vertex<T>> currentPath, List<List<Vertex<T>>> paths, List<Edge<T, U>> currentEdgeee, List<List<Edge<T, U>>> edgess)
//     {
//         if (currentVertex == endVertex && currentWeight == totalWeight)
//         {
//             paths.Add(new List<Vertex<T>>(currentPath));
//             edgess.Add(new List<Edge<T, U>>(currentEdgeee));
//             currentEdgeee.Clear();
//
//             return;
//         }
//
//         if (currentWeight >= totalWeight)
//             return;
//
//         foreach (Edge<T, U> edge in adjacencyList[currentVertex])
//         {
//             Vertex<T> neighbor = edge.Destination;
//             long edgeWeight = edge.Weight;
//             long newWeight = currentWeight + edgeWeight;
//
//             currentPath.Add(neighbor);
//             currentEdgeee.Add(edge);
//             DFS(neighbor, endVertex, totalWeight, newWeight, currentPath, paths, currentEdgeee,edgess);
//             currentPath.RemoveAt(currentPath.Count - 1);
//         }
//     }
// }
//
// public class GraphPrintAccount<T, U>
// {
//     private Dictionary<Vertex<T>, List<Edge<T, U>>> adjacencyList;
//
//     public GraphPrintAccount()
//     {
//         adjacencyList = new Dictionary<Vertex<T>, List<Edge<T, U>>>();
//     }
//
//     public void AddVertex(Vertex<T> vertex)
//     {
//         adjacencyList[vertex] = new List<Edge<T, U>>();
//     }
//
//     public void AddEdge(Vertex<T> source, Vertex<T> destination, U information, long weight)
//     {
//         Edge<T, U> edge = new Edge<T, U>(source, destination, information, weight);
//         adjacencyList[source].Add(edge);
//     }
//
//     public List<List<Vertex<T>>> FindPaths(Vertex<T> startVertex, Vertex<T> endVertex, int numEdges)
//     {
//         List<List<Vertex<T>>> paths = new List<List<Vertex<T>>>();
//         List<Vertex<T>> currentPath = new List<Vertex<T>>();
//
//         currentPath.Add(startVertex);
//         DFS(startVertex, endVertex, numEdges, 0, currentPath, paths);
//
//         return paths;
//     }
//
//     private void DFS(Vertex<T> currentVertex, Vertex<T> endVertex, long totalWeight, long currentWeight,
//         List<Vertex<T>> currentPath, List<List<Vertex<T>>> paths)
//     {
//         if (currentVertex == endVertex && currentWeight == totalWeight)
//         {
//             paths.Add(new List<Vertex<T>>(currentPath));
//
//             return;
//         }
//
//         if (currentWeight >= totalWeight)
//             return;
//
//         foreach (Edge<T, U> edge in adjacencyList[currentVertex])
//         {
//             Vertex<T> neighbor = edge.Destination;
//             long edgeWeight = edge.Weight;
//             long newWeight = currentWeight + edgeWeight;
//
//             currentPath.Add(neighbor);
//             DFS(neighbor, endVertex, totalWeight, newWeight, currentPath, paths);
//             currentPath.RemoveAt(currentPath.Count - 1);
//         }
//     }
// }
//
//
// public class GraphPrintTransaction<T, U>
// {
//     private Dictionary<Vertex<T>, List<Edge<T, U>>> adjacencyList;
//
//     public GraphPrintTransaction()
//     {
//         adjacencyList = new Dictionary<Vertex<T>, List<Edge<T, U>>>();
//     }
//
//     public void AddVertex(Vertex<T> vertex)
//     {
//         adjacencyList[vertex] = new List<Edge<T, U>>();
//     }
//
//     public void AddEdge(Vertex<T> source, Vertex<T> destination, U information, long weight)
//     {
//         Edge<T, U> edge = new Edge<T, U>(source, destination, information, weight);
//         adjacencyList[source].Add(edge);
//     }
//
//     public List<List<Edge<T, U>>> FindPaths(Vertex<T> startVertex, Vertex<T> endVertex, int numEdges)
//     {
//         
//         List<List<Edge<T, U>>> edgess = new List<List<Edge<T, U>>>();
//         List<Edge<T, U>> currentEdgeee = new List<Edge<T, U>>();
//
//
//
//         DFS(startVertex, endVertex, numEdges, 0, currentEdgeee, edgess);
//         
//         return edgess;
//     }
//
//     private void DFS(Vertex<T> currentVertex, Vertex<T> endVertex, long totalWeight, long currentWeight, List<Edge<T, U>> currentEdgeee, List<List<Edge<T, U>>> edgess)
//     {
//         if (currentVertex == endVertex && currentWeight == totalWeight)
//         {
//             edgess.Add(new List<Edge<T, U>>(currentEdgeee));
//             currentEdgeee.Clear();
//
//             return;
//         }
//
//         if (currentWeight >= totalWeight)
//             return;
//
//         foreach (Edge<T, U> edge in adjacencyList[currentVertex])
//         {
//             Vertex<T> neighbor = edge.Destination;
//             long edgeWeight = edge.Weight;
//             long newWeight = currentWeight + edgeWeight;
//
//             currentEdgeee.Add(edge);
//             DFS(neighbor, endVertex, totalWeight, newWeight, currentEdgeee,edgess);
//             // currentPath.RemoveAt(currentPath.Count - 1);
//         }
//     }
// }
