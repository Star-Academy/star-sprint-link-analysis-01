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
        graph.AddVertex(_accounts[0]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 0,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[0], transaction0, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[0], 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheSameWeight_ReturnsTwoPath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 1000);
        graph.AddEdge(_accounts[0], _accounts[1], transaction1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[1], 1000);

        Assert.Equal(2, paths.Count);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheSameWeightAndInTheOppositeDirection_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 1,
            DestiantionAccount = 0,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 1000);
        graph.AddEdge(_accounts[1], _accounts[0], transaction1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[1], 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_TwoPathsFromOneVertexToAnotherWithTheDifferentWeight_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 10000);
        graph.AddEdge(_accounts[0], _accounts[1], transaction1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[1], 1000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_ThreePathsFromOneVertexToAnotherWithTheOneDifferentWeight_ReturnsTwoPath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();


        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 10000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction2 = new Transaction
        {
            ID = 2,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 10000);
        graph.AddEdge(_accounts[0], _accounts[1], transaction1, 1000);
        graph.AddEdge(_accounts[0], _accounts[1], transaction2, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[1], 1000);

        Assert.Equal(2, paths.Count);
    }

    [Fact]
    public void FindPaths_3Vertexes1_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        graph.AddVertex(_accounts[2]);
        Transaction transaction2 = new Transaction
        {
            ID = 2,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction3 = new Transaction
        {
            ID = 3,
            SourceAcount = 2,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction2, 1000);
        graph.AddEdge(_accounts[2], _accounts[1], transaction3, 1000);


        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[2], 2000);

        Assert.Empty(paths);
    }

    [Fact]
    public void FindPaths_3Vertexes2_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        graph.AddVertex(_accounts[2]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 1,
            DestiantionAccount = 2,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 1000);
        graph.AddEdge(_accounts[1], _accounts[2], transaction1, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[2], 2000);

        Assert.Single(paths);
    }

    [Fact]
    public void FindPaths_3Vertexes3_ReturnsOnePath()
    {
        Graph<Account, Transaction> graph = new Graph<Account, Transaction>();
        graph.AddVertex(_accounts[0]);
        graph.AddVertex(_accounts[1]);
        graph.AddVertex(_accounts[2]);
        Transaction transaction0 = new Transaction
        {
            ID = 0,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction1 = new Transaction
        {
            ID = 1,
            SourceAcount = 1,
            DestiantionAccount = 2,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction2 = new Transaction
        {
            ID = 2,
            SourceAcount = 0,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        Transaction transaction3 = new Transaction
        {
            ID = 3,
            SourceAcount = 2,
            DestiantionAccount = 1,
            TransactionType = TransactionType.Paya,
            Amount = 1000,
            Date = DateTime.Now,
        };
        graph.AddEdge(_accounts[0], _accounts[1], transaction0, 1000);
        graph.AddEdge(_accounts[1], _accounts[2], transaction1, 1000);
        graph.AddEdge(_accounts[0], _accounts[1], transaction2, 1000);
        graph.AddEdge(_accounts[2], _accounts[1], transaction3, 1000);

        List<List<Edge<Account, Transaction>>> paths = graph.FindPaths(_accounts[0], _accounts[2], 2000);

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

public class GraphCreator<TVertex, TEdge> where TVertex : notnull
{
    private List<TVertex> Vertexes { get; set; }
    private List<Edge<TVertex, TEdge>> Edges { get; set; }

    public GraphCreator(List<TVertex> vertexes, List<Edge<TVertex, TEdge>> edges)
    {
        Vertexes = vertexes;
        Edges = edges;
    }

    public Graph<TVertex, TEdge> CreateGraph()
    {
        Graph<TVertex, TEdge> graph = new Graph<TVertex, TEdge>();

        foreach (TVertex vertex in Vertexes)
        {
            graph.AddVertex(vertex);
        }

        foreach (Edge<TVertex, TEdge> edge in Edges)
        {
            graph.AddEdge(edge.Source, edge.Destination, edge.Information, edge.Weight);
        }

        return graph;
    }
}

public class Edge<TVertex, TEdge>
{
    public TVertex Source { get; }
    public TVertex Destination { get; }
    public TEdge Information { get; }
    public decimal Weight { get; set; }

    public Edge(TVertex source, TVertex destination, TEdge information, decimal weight)
    {
        Source = source;
        Destination = destination;
        Information = information;
        Weight = weight;
    }
}

public class Graph<TVertex, TEdge> where TVertex : notnull
{
    private readonly Dictionary<TVertex, List<Edge<TVertex, TEdge>>> _adjacencyList;

    public Graph()
    {
        _adjacencyList = new Dictionary<TVertex, List<Edge<TVertex, TEdge>>>();
    }

    public void AddVertex(TVertex vertex)
    {
        _adjacencyList[vertex] = new List<Edge<TVertex, TEdge>>();
    }

    public void AddEdge(TVertex source, TVertex destination, TEdge information, decimal weight)
    {
        _adjacencyList[source].Add(new Edge<TVertex, TEdge>(source, destination, information, weight));
    }

    public List<List<Edge<TVertex, TEdge>>> FindPaths(TVertex startVertex, TVertex endVertex, int weightOfPaths)
    {
        List<List<Edge<TVertex, TEdge>>> paths = new List<List<Edge<TVertex, TEdge>>>();
        List<Edge<TVertex, TEdge>> currentPath = new List<Edge<TVertex, TEdge>>();


        DepthFirstSearch(startVertex, endVertex, weightOfPaths, 0, currentPath, paths);

        return paths;
    }

    private void DepthFirstSearch(
        TVertex currentVertex,
        TVertex endVertex,
        decimal weightOfPaths,
        decimal currentWeightOfPaths,
        List<Edge<TVertex, TEdge>> currentPath,
        List<List<Edge<TVertex, TEdge>>> paths
    )
    {
        if (currentVertex.Equals(endVertex) && currentWeightOfPaths == weightOfPaths)
        {
            paths.Add(new List<Edge<TVertex, TEdge>>(currentPath));

            currentPath.Clear();
        }
        else
        {
            foreach (Edge<TVertex, TEdge> edge in _adjacencyList[currentVertex])
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