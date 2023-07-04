using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizerTest;

public class MaxFlowTest
{
    private readonly List<Account> _accounts;

    public MaxFlowTest()
    {
        Branch branch = new Branch()
        {
            Id = 0,
            Address = "0",
            Name = "0",
            Telephone = "0"
        };
        Owner owner = new Owner()
        {
            ID = 0,
            Name = "0",
            FamilyName = "0",
        };
        _accounts = new List<Account>()
        {
            new Account()
            {
                AccountID = 0,
                CardID = "0",
                Sheba = "0",
                AccountType = AccountType.Jari,
                Branch = branch,
                Owner = owner,
            },
            new Account()
            {
                AccountID = 1,
                CardID = "1",
                Sheba = "1",
                AccountType = AccountType.Jari,
                Branch = branch,
                Owner = owner,
            },
            new Account()
            {
                AccountID = 2,
                CardID = "2",
                Sheba = "2",
                AccountType = AccountType.Jari,
                Branch = branch,
                Owner = owner,
            },
            new Account()
            {
                AccountID = 3,
                CardID = "3",
                Sheba = "3",
                AccountType = AccountType.Jari,
                Branch = branch,
                Owner = owner,
            },
        };
    }

    [Fact]
    public void FindPaths_LoopForGraphWitheOneVertex_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        graph.AddVertex(account_0);
        Transaction transaction_0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 0,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(account_0, account_0, transaction_0, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_0, 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheSameWeight_ReturnsTwoPath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];

        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
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
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(account_0, account_1, transaction_0, 1000);
        graph.AddEdge(account_0, account_1, transaction_1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_1, 1000);

        Assert.Equal(2, paths.Count);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheSameWeightAndInTheOppositeDirection_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
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
            DestiantionAccount = 0,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(account_0, account_1, transaction_0, 1000);
        graph.AddEdge(account_1, account_0, transaction_1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_1, 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheDifferentWeight_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
        Transaction transaction_0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        Transaction transaction_1 = new Transaction
        {
            ID = 1,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(account_0, account_1, transaction_0, 10000);
        graph.AddEdge(account_0, account_1, transaction_1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_1, 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_ThreePathsFromOneVertexToAnotherWithTheOneDifferentWeight_ReturnsTwoPath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
        Transaction transaction_0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        Transaction transaction_1 = new Transaction
        {
            ID = 1,
            SourceAcount = 0,
            DestiantionAccount = 1,
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
        graph.AddEdge(account_0, account_1, transaction_0, 10000);
        graph.AddEdge(account_0, account_1, transaction_1, 1000);
        graph.AddEdge(account_0, account_1, transaction_2, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_1, 1000);

        Assert.Equal(2, paths.Count);
    }

    [Fact]
    public void FindPaths_3Vertexes1_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        Account account_2 = _accounts[2];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
        graph.AddVertex(account_2);
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
        Transaction transaction_4 = new Transaction
        {
            ID = 4,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        Transaction transaction_5 = new Transaction
        {
            ID = 5,
            SourceAcount = 1,
            DestiantionAccount = 2,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        graph.AddEdge(account_0, account_1, transaction_2, 1000);
        graph.AddEdge(account_2, account_1, transaction_3, 1000);


        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_2, 2000);

        Assert.Empty(paths);
    }

    [Fact]
    public void FindPaths_3Vertexes2_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        Account account_2 = _accounts[2];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
        graph.AddVertex(account_2);
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
        graph.AddEdge(account_0, account_1, transaction_0, 1000);
        graph.AddEdge(account_1, account_2, transaction_1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(account_0, account_2, 2000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_3Vertexes3_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        Account account_0 = _accounts[0];
        Account account_1 = _accounts[1];
        Account account_2 = _accounts[2];
        graph.AddVertex(account_0);
        graph.AddVertex(account_1);
        graph.AddVertex(account_2);
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
        graph.AddEdge(account_0, account_1, transaction_0, 1000);
        graph.AddEdge(account_1, account_2, transaction_1, 1000);
        graph.AddEdge(account_0, account_1, transaction_2, 1000);
        graph.AddEdge(account_2, account_1, transaction_3, 1000);

        List<long> acc = new List<long>();
        acc.Add(0);
        acc.Add(1);
        acc.Add(2);
        
        List<Transaction> trs = new List<Transaction>();
        trs.Add(transaction_0);
        trs.Add(transaction_1);
        trs.Add(transaction_2);
        trs.Add(transaction_3);
        
        Graph<long, Transaction> transactionsNetworkGraphCreator = new TransactionsNetworkGraphCreator(
            acc,
            trs
            ).CreateTransactionsNetworkGraph();

            List<List<Edge<long, Transaction>>> paths = transactionsNetworkGraphCreator.FindPaths(0, 2, 2000);

        Assert.Equal(2, paths.Count);
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

// public class TransactionsNetworkGraphCreator
// {
//     private List<long> _accounts;
//     private List<Transaction> _transactions;
//
//     public TransactionsNetworkGraphCreator(List<long> accounts, List<Transaction> transactions)
//     {
//         _accounts = accounts;
//         _transactions = transactions;
//     }
//
//     public Graph<long, Transaction> CreateTransactionsNetworkGraph()
//     {
//         List<Edge<long, Transaction>> edges = new List<Edge<long, Transaction>>();
//         foreach (Transaction edge in _transactions)
//         {
//             edges.Add(new Edge<long, Transaction>(new Vertex<long>(edge.SourceAcount), new Vertex<long>(edge.DestiantionAccount), edge, edge.Amount));
//         }
//         return new GraphCreator<long, Transaction>(_accounts, edges).CreateGraph();
//     }
// }
//
// public class GraphCreator<T, U>
// {
//     private List<T> Vertexes { get; set; }
//     private List<Edge<T, U>> Edges { get; set; }
//
//     public GraphCreator(List<T> vertexes, List<Edge<T, U>> edges)
//     {
//         Vertexes = vertexes;
//         Edges = edges;
//     }
//
//     public Graph<T, U> CreateGraph()
//     {
//         Graph<T, U> graph = new Graph<T, U>();
//
//         foreach (T vertex in Vertexes)
//         {
//             graph.AddVertex(new Vertex<T>(vertex));
//         }
//
//         foreach (Edge<T, U> edge in Edges)
//         {
//             graph.AddEdge(edge.Source, edge.Destination, edge.Information, edge.Weight);
//         }
//
//         return graph;
//     }
// }
//
// public class Vertex<T>
// {
//     public T Data { get; set; }
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
//     public decimal Weight { get; set; }
//
//     public Edge(Vertex<T> source, Vertex<T> destination, U information, decimal weight)
//     {
//         Source = source;
//         Destination = destination;
//         Information = information;
//         Weight = weight;
//     }
// }
//
// public class Graph<T, U>
// {
//     private readonly Dictionary<Vertex<T>, List<Edge<T, U>>> _adjacencyList;
//
//     public Graph()
//     {
//         _adjacencyList = new Dictionary<Vertex<T>, List<Edge<T, U>>>();
//     }
//
//     public void AddVertex(Vertex<T> vertex)
//     {
//         _adjacencyList[vertex] = new List<Edge<T, U>>();
//     }
//
//     public void AddEdge(Vertex<T> source, Vertex<T> destination, U information, decimal weight)
//     {
//         _adjacencyList[source].Add(new Edge<T, U>(source, destination, information, weight));
//     }
//
//     public List<List<Edge<T, U>>> FindPaths(Vertex<T> startVertex, Vertex<T> endVertex, int weightOfPaths)
//     {
//         List<List<Edge<T, U>>> paths = new List<List<Edge<T, U>>>();
//         List<Edge<T, U>> currentPath = new List<Edge<T, U>>();
//
//
//         DepthFirstSearch(startVertex, endVertex, weightOfPaths, 0, currentPath, paths);
//
//         return paths;
//     }
//
//     private void DepthFirstSearch(
//         Vertex<T> currentVertex,
//         Vertex<T> endVertex,
//         decimal weightOfPaths,
//         decimal currentWeightOfPaths,
//         List<Edge<T, U>> currentPath,
//         List<List<Edge<T, U>>> paths
//     )
//     {
//         if (currentVertex == endVertex && currentWeightOfPaths == weightOfPaths)
//         {
//             paths.Add(new List<Edge<T, U>>(currentPath));
//
//             currentPath.Clear();
//         }
//         else
//         {
//             foreach (Edge<T, U> edge in _adjacencyList[currentVertex])
//             {
//                 currentPath.Add(edge);
//
//                 DepthFirstSearch(edge.Destination,
//                     endVertex,
//                     weightOfPaths,
//                     currentWeightOfPaths + edge.Weight,
//                     currentPath,
//                     paths
//                 );
//             }
//         }
//     }
// }