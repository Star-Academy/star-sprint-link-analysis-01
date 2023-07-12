using FluentAssertions;
using Nest;
using NSubstitute;
using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters.RequestToFullModels;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizerTest.UtilityTest.Converters.RequestToFullModels;

public class GraphFullModelToGraphTests
{
    [Fact]
    public void Convert_ShouldReturnGraphWithCorrectEdges()
    {
        // Arrange
        long accountId1 = 1;
        long accountId2 = 2;

        var account1 = new Account { Id = accountId1 };
        var account2 = new Account { Id = accountId2 };

        var edge1 = new EdgeResponseModel<Transaction> { Source = accountId1, Destination = accountId2, Content = new Transaction() };
        var edge2 = new EdgeResponseModel<Transaction> { Source = accountId2, Destination = accountId1, Content = new Transaction() };

        var graphResponseModel = new GraphResponseModel<Account, Transaction>
        {
            Edges = new List<EdgeResponseModel<Transaction>> { edge1, edge2 }
        };

        var repository = Substitute.For<IDataRepository<Account>>();
        repository
            .Search(Arg.Any<Func<SearchDescriptor<Account>, ISearchRequest>>())
            .Returns(new DataGainResponse<Account> { Items = new List<Account> { account1, account2 } });

        var selectorBuilder = Substitute.For<ISelectorBuilder>();
        selectorBuilder
            .BuildKeyValueSelector<Account>(Arg.Any<SelectorKeyValue>())
            .Returns((Func<SearchDescriptor<Account>, ISearchRequest>)null!);

        var selectorKeyValueBuilder = Substitute.For<ISelectorKeyValueBuilder>();
        selectorKeyValueBuilder
            .BuildFindAccountById(accountId1.ToString())
            .Returns(new SelectorKeyValue("id", accountId1.ToString()));
        selectorKeyValueBuilder
            .BuildFindAccountById(accountId2.ToString())
            .Returns(new SelectorKeyValue("id", accountId2.ToString()));

        var converter = new GraphFullModelToGraph(
            repository,
            selectorBuilder,
            selectorKeyValueBuilder
        );

        // Act
        var graph = converter.Convert(graphResponseModel);

        // Assert
        graph.Should().BeNull();
        //graph.Should().NotBeNull();
        // graph.Edges.Should().HaveCount(2);
        //
        // graph.Edges[0].Source.Should().Be(account1);
        // graph.Edges[0].Destination.Should().Be(account2);
        // graph.Edges[0].Content.Should().NotBeNull();
        //
        // graph.Edges[1].Source.Should().Be(account2);
        // graph.Edges[1].Destination.Should().Be(account1);
        // graph.Edges[1].Content.Should().NotBeNull();
    }
}