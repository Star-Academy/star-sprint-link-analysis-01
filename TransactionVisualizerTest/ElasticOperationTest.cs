using Elasticsearch.Net;
using Nest;
using TransactionVisualizer.Models.Transaction;
using Xunit.Abstractions;

namespace TransactionVisualizerTest;

public class ElasticOperationTest
{
    private ITestOutputHelper _testOutputHelper;
    private IElasticOperation _elasticOperation;
    private List<Transaction> _transactions;

    public ElasticOperationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        const string url = "http://localhost:9200";
        const string indexName = "transaction-test9";

        _elasticOperation = new ElasticOperation(url, indexName);
        _transactions = new List<Transaction>()
        {
            new Transaction
            {
                ID = 1,
                SourceAcount = 1,
                DestiantionAccount = 2,
                TransactionType = TransactionType.Satna,
                Amount = 100000,
                Date = DateTime.Parse("2021-01-01"),
            },
            new Transaction
            {
                ID = 2,
                SourceAcount = 1,
                DestiantionAccount = 2,
                TransactionType = TransactionType.Satna,
                Amount = 100000,
                Date = DateTime.Parse("2021-01-01"),
            },
            new Transaction
            {
                ID = 3,
                SourceAcount = 2,
                DestiantionAccount = 3,
                TransactionType = TransactionType.Satna,
                Amount = 100000,
                Date = DateTime.Parse("2021-01-01"),
            },
        };
    }

    [Fact]
    public void IndexBulk_NullParameter_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _elasticOperation.IndexBulk<Transaction>(null!));
    }

    [Fact]
    public void IndexBulk_EmptyListParameter_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _elasticOperation.IndexBulk(new List<Transaction>()));
    }

    [Fact]
    public void IndexBulk_ListWithoutDuplicateElement_IsValidBeTrue()
    {
        BulkResponse response = _elasticOperation.IndexBulk(_transactions);

        Assert.False(response.Errors);
    }

    [Fact]
    public void IndexDocument_NullParameter_NullReferenceException()
    {
        IndexResponse response = _elasticOperation.IndexDocument<Transaction>(new IndexRequest<Transaction>());

        Assert.False(response.IsValid);
    }

    [Fact]
    public void IndexDocument_NewDocument_IsValidBeTrue()
    {
        long id = new Random().Next(_transactions.Count, int.MaxValue);

        IndexResponse response = _elasticOperation.IndexDocument<Transaction>(
            new IndexRequest<Transaction>(
                new Transaction
                {
                    ID = id,
                    SourceAcount = 1,
                    DestiantionAccount = 2,
                    TransactionType = TransactionType.Satna,
                    Amount = 100000,
                    Date = DateTime.Parse("2021-01-01"),
                }
            )
        );

        Assert.True(response.IsValid);
    }

    [Fact]
    public void SearchDocument_NotExistDocument_IsNull()
    {
        string id = "100";
        ISearchResponse<Transaction> transaction = _elasticOperation.SearchDocument<Transaction>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.ID)
                    .Query(id)
                )
            )
        );

        Assert.Equal(0, transaction.Hits.Count);
    }

    [Fact]
    public void SearchDocument_ExistDocument_IsNotNull()
    {
        string id = "1";
        ISearchResponse<Transaction> transaction = _elasticOperation.SearchDocument<Transaction>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.ID)
                    .Query(id)
                )
            )
        );

        Assert.Equal(id, transaction.Hits.ToArray()[0].Id);
    }
}