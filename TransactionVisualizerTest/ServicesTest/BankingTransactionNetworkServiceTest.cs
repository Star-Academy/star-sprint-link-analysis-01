using FluentAssertions;
using NSubstitute;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Services;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.ServicesTest;

public class BankingTransactionNetworkServiceTests
{
    private readonly IDataRepository<Transaction> _edgeRepository;
    private readonly IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>> _requestToFull;
    private readonly IModelToGraphEdge<Transaction, Account, Transaction> _modelToGraphEdge;
    private readonly IExpander<Account, Transaction> _expander;
    private readonly IMaxFlowCalculator<Account, Transaction> _maxFlowCalculator;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;

    private readonly BankingTransactionNetworkService _networkService;

    public BankingTransactionNetworkServiceTests()
    {
        _edgeRepository = Substitute.For<IDataRepository<Transaction>>();
        _requestToFull = Substitute.For<IRequestToFullModel<GraphResponseModel<Account, Transaction>, Graph<Account, Transaction>>>();
        _modelToGraphEdge = Substitute.For<IModelToGraphEdge<Transaction, Account, Transaction>>();
        _expander = Substitute.For<IExpander<Account, Transaction>>();
        _maxFlowCalculator = Substitute.For<IMaxFlowCalculator<Account, Transaction>>();
        _selectorBuilder = Substitute.For<ISelectorBuilder>();
        _selectorKeyValueBuilder = Substitute.For<ISelectorKeyValueBuilder>();

        _networkService = new BankingTransactionNetworkService(
            _edgeRepository,
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
        var expandRequest = new ExpandRequestModel<Account, Transaction>();
        var maxLength = 10;
        var vertex = new Account();
        var expandedGraph = new Graph<Account, Transaction>();
        _expander.Expand(maxLength, vertex, _networkService.GetState()).Returns(expandedGraph);

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
        var edgeRepositoryResponse = new DataGainResponse<Transaction> { Items = new List<Transaction>(transactionList) };
        _edgeRepository.Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>()).Returns(edgeRepositoryResponse);
        _modelToGraphEdge.Convert(transaction).Returns(new Edge<Account, Transaction>());

        // Act
        var result = _networkService.InitialGraph(accountId);

        // Assert
        result.Should().BeSameAs(graph);
        _modelToGraphEdge.Received(1).Convert(transaction);
        _edgeRepository.Received(1).Search(Arg.Any<System.Func<Nest.SearchDescriptor<Transaction>, Nest.ISearchRequest>>());
    }
}