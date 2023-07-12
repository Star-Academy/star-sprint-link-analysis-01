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
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.ServicesTest;

public class BankingTransactionNetworkServiceTests
{
    private readonly IElasticDataRepositoryBuilder<Transaction> _edgeRepositoryBuilder;

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
        _edgeRepositoryBuilder = Substitute.For<TransactionRepositoryBuilder>();
        _requestToFull = Substitute
            .For<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>>();
        _modelToGraphEdge = Substitute.For<IModelToGraphEdge<Transaction, Account, Transaction>>();
        _expander = Substitute.For<IExpander<Account, Transaction>>();
        _maxFlowCalculator = Substitute.For<IMaxFlowCalculator<Account, Transaction>>();
        _selectorBuilder = Substitute.For<ISelectorBuilder>();
        _selectorKeyValueBuilder = Substitute.For<ISelectorKeyValueBuilder>();

        _networkService = new BankingTransactionNetworkService(
            _edgeRepositoryBuilder,
            _requestToFull,
            _modelToGraphEdge,
            _expander,
            _maxFlowCalculator,
            _selectorBuilder,
            _selectorKeyValueBuilder
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
        var maxLength = 1;
        var initGraph = new Graph<Account, Transaction>();

        var account1 = new Account() { Id = 1 };
        var account2 = new Account() { Id = 2 };
        var account3 = new Account() { Id = 3 };


        var graphResponseModel = new GraphResponseModel<Account, Transaction>
        {
            Vertices = new List<Account>
            {
                account1,
                account2
            },
            Edges = new List<EdgeResponseModel<Transaction>>
            {
                new EdgeResponseModel<Transaction>()
                {
                    Destination = 2,
                    Source = 1
                }
            },
        };

        var expandRequest = new ExpandRequestModel<Account, Transaction>()
        {
            CurrentState = graphResponseModel,
            MaxLength = maxLength,
            Vertex = account2
        };


        .Search(Arg.Any<Func<SearchDescriptor<Transaction>, ISearchRequest>>())
            .Returns(new DataGainResponse<Transaction>()
            {
                Items = new List<Transaction>()
                {
                    new Transaction()
                    {
                        SourceAccount = 2,
                        DestinationAccount = 3
                    }
                }
            });

        var expandedGraph = new Graph<Account, Transaction>();
        expandedGraph.AddEdge(new Edge<Account, Transaction>()
        {
            Destination = account1,
            Source = account2
        });
        expandedGraph.AddEdge(new Edge<Account, Transaction>()
        {
            Destination = account2,
            Source = account3
        });


        // Act
        var result = _networkService.Expand(expandRequest);

        // Assert
        result.Should().BeSameAs(expandedGraph);
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
        _edgeRepository.Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>())
            .Returns(edgeRepositoryResponse);
        _modelToGraphEdge.Convert(transaction).Returns(new Edge<Account, Transaction>());

        // Act
        var result = _networkService.InitialGraph(accountId);

        // Assert
        result.Should().BeSameAs(graph);
        _modelToGraphEdge.Received(1).Convert(transaction);
        _edgeRepository.Received(1)
            .Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>());
    }
}