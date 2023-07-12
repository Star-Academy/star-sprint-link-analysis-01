using FluentAssertions;
using Nest;
using NSubstitute;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Services.Graph;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Builders.ResponseModelBuilder;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.ServicesTest;

public class BankingTransactionNetworkServiceTests
{
    private readonly IElasticDataRepositoryBuilder<Transaction> _edgeRepositoryBuilder;
    private readonly IDataRepository<Transaction> _dataRepository;

    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>
        _requestToFull;

    private readonly IModelToGraphEdge<Transaction, Account, Transaction> _modelToGraphEdge;
    private readonly IExpander<Account, Transaction> _expander;
    private readonly IMaxFlowCalculator<Account, Transaction> _maxFlowCalculator;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;

    private readonly BankingTransactionNetworkService _networkService;

    public BankingTransactionNetworkServiceTests()
    {
        _edgeRepositoryBuilder = Substitute.For<IElasticDataRepositoryBuilder<Transaction>>();
        _requestToFull = Substitute
            .For<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>>();
        _modelToGraphEdge = Substitute.For<IModelToGraphEdge<Transaction, Account, Transaction>>();
        _expander = Substitute.For<IExpander<Account, Transaction>>();
        _maxFlowCalculator = Substitute.For<IMaxFlowCalculator<Account, Transaction>>();
        _selectorBuilder = Substitute.For<ISelectorBuilder>();
        _selectorKeyValueBuilder = Substitute.For<ISelectorKeyValueBuilder>();

        _dataRepository = Substitute.For<IDataRepository<Transaction>>();
        _edgeRepositoryBuilder.Build().Returns(_dataRepository);

        _networkService = new BankingTransactionNetworkService(
            _edgeRepositoryBuilder,
            _requestToFull,
            _modelToGraphEdge,
            _expander,
            _maxFlowCalculator,
            _selectorBuilder,
            _selectorKeyValueBuilder,
            new GraphResponseModelBuilder()
        );
    }

    [Fact]
    public void SetState_ShouldSetGraph()
    {
        // Arrange
        var graph = new GraphResponseModel<Account, Transaction>();
        var convertedGraph = new Graph<Account, Transaction>();
        _requestToFull.Convert(graph).Returns(convertedGraph);

        // Act
        _networkService.SetState(graph);

        // Assert
        _networkService.GetState().Should().BeSameAs(convertedGraph);
    }

    [Fact]
    public void GetState_ShouldReturnGraph()
    {
        // Arrange
        var graph = new GraphResponseModel<Account, Transaction>();
        var convertedGraph = new Graph<Account, Transaction>();
        _requestToFull.Convert(graph).Returns(convertedGraph);
        _networkService.SetState(graph);

        // Act
        var result = _networkService.GetState();

        // Assert
        result.Should().BeSameAs(convertedGraph);
    }

    [Fact]
    public void Expand_ShouldCallExpander_WhenReturnExpandedGraph()
    {
        // Arrange
        var account0 = new Account() { Id = 0 };
        var account1 = new Account() { Id = 1 };
        var account2 = new Account() { Id = 2 };
        var account3 = new Account() { Id = 3 };
        var account4 = new Account() { Id = 4 };
        var account5 = new Account() { Id = 5 };
        
        var transaction0 = new Transaction() { Id = 0, SourceAccount = 0, DestinationAccount = 1, Amount = 1 };
        var transaction1 = new Transaction() { Id = 0, SourceAccount = 1, DestinationAccount = 1, Amount = 1 };
        var transaction2 = new Transaction() { Id = 0, SourceAccount = 2, DestinationAccount = 1, Amount = 1 };
        var transaction3 = new Transaction() { Id = 0, SourceAccount = 3, DestinationAccount = 1, Amount = 1 };
        var transaction4 = new Transaction() { Id = 3, SourceAccount = 4, DestinationAccount = 1, Amount = 1 };
        var transaction5 = new Transaction() { Id = 0, SourceAccount = 5, DestinationAccount = 1, Amount = 1 };
        var transaction6 = new Transaction() { Id = 5, SourceAccount = 4, DestinationAccount = 1, Amount = 1 };
        
        var edge0 = new Edge<Account, Transaction>(){Source = account0, Destination = account0, Content = transaction0, Weight = transaction0.Amount};
        var edge1 = new Edge<Account, Transaction>(){Source = account0, Destination = account1, Content = transaction1, Weight = transaction1.Amount};
        var edge2 = new Edge<Account, Transaction>(){Source = account0, Destination = account2, Content = transaction2, Weight = transaction2.Amount};
        var edge3 = new Edge<Account, Transaction>(){Source = account0, Destination = account3, Content = transaction3, Weight = transaction3.Amount};
        var edge4 = new Edge<Account, Transaction>(){Source = account3, Destination = account4, Content = transaction4, Weight = transaction4.Amount};
        var edge5 = new Edge<Account, Transaction>(){Source = account0, Destination = account5, Content = transaction5, Weight = transaction5.Amount};
        var edge6 = new Edge<Account, Transaction>(){Source = account5, Destination = account4, Content = transaction6, Weight = transaction6.Amount};
        
        var graph = new Graph<Account, Transaction>();
        graph.AddEdge(edge0);
        graph.AddEdge(edge1);
        graph.AddEdge(edge2);
        graph.AddEdge(edge3);
        graph.AddEdge(edge4);
        graph.AddEdge(edge5);
        graph.AddEdge(edge6);

        int maxLength = 2;
        var graphResponseModel = new GraphResponseModel<Account, Transaction>
        {
            Vertices = new List<Account>
            {
                account0,
                account1,
                account2,
                account3,
                account4,
                account5,
            },
            Edges = new List<EdgeResponseModel<Transaction>>
            {
                new EdgeResponseModel<Transaction>() { Source = account0.Id, Destination = account0.Id, Content = transaction0, },
                new EdgeResponseModel<Transaction>() { Source = account0.Id, Destination = account1.Id, Content = transaction1, },
                new EdgeResponseModel<Transaction>() { Source = account0.Id, Destination = account2.Id, Content = transaction2, },
                new EdgeResponseModel<Transaction>() { Source = account0.Id, Destination = account3.Id, Content = transaction3, },
                new EdgeResponseModel<Transaction>() { Source = account3.Id, Destination = account4.Id, Content = transaction4, },
                new EdgeResponseModel<Transaction>() { Source = account0.Id, Destination = account5.Id, Content = transaction5, },
                new EdgeResponseModel<Transaction>() { Source = account5.Id, Destination = account4.Id, Content = transaction6, },
            },
        };

        var expandRequest = new ExpandRequestModel<Account, Transaction>()
        {
            CurrentState = graphResponseModel,
            MaxLength = maxLength,
            Vertex = account0
        };
        // _dataRepository.Search(Arg.Any<Func<SearchDescriptor<Transaction>, ISearchRequest>>())
        //     .Returns(new DataGainResponse<Transaction>()
        //     {
        //         Items = new List<Transaction>()
        //         {
        //             transaction0,
        //             transaction1,
        //             transaction2,
        //             transaction3,
        //             transaction4,
        //             transaction5,
        //             transaction6
        //         }
        //     });
        //
        // var expandedGraph = new Graph<Account, Transaction>();
        // expandedGraph.AddEdge(new Edge<Account, Transaction>()
        // {
        //     Destination = account1,
        //     Source = account2
        // });
        // expandedGraph.AddEdge(new Edge<Account, Transaction>()
        // {
        //     Destination = account2,
        //     Source = account3
        // });

       Graph<Account, Transaction> x = _networkService.Expand(expandRequest);





        // Act
        //var result = _networkService.Expand(expandRequest);

        // Assert
        //Assert.Equal(2, x.AdjacencyMatrix.Count);
    }

    [Fact]
    public void MaxFlowCalculator_ShouldCallMaxFlowCalculatorAndReturnMaxFlow()
    {
        // Arrange
        var maxFlowRequest = new MaxFlowCalculatorRequestModel<Account, Transaction>();
        var source = new Account();
        var destination = new Account();
        var maxFlow = 100.0m;
        _maxFlowCalculator.Calculate(source, destination, _networkService.GetState()).Returns(maxFlow);

        // Act
        var result = _networkService.MaxFlowCalculator(maxFlowRequest);

        // Assert
        result.Should().Be(maxFlow);
    }

    [Fact]
    public void InitialGraph_ShouldSetGraphWithEdgesFromRepository()
    {
        // Arrange
        var accountId = 1L;
        var transaction = new Transaction { SourceAccount = accountId };
        var transactionList = new[] { transaction };
        var graph = new Graph<Account, Transaction>();
        var edgeRepositoryResponse = new DataGainResponse<Transaction>
            { Items = new List<Transaction>(transactionList) };
        _dataRepository.Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>())
            .Returns(edgeRepositoryResponse);
        _modelToGraphEdge.Convert(transaction).Returns(new Edge<Account, Transaction>());

        // Act
        var result = _networkService.InitialGraph(accountId);

        // Assert
        result.Should().BeSameAs(graph);
        _modelToGraphEdge.Received(1).Convert(transaction);
        _dataRepository.Received(1)
            .Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>());
    }
}