using FluentAssertions;
using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class ElasticDataRepositoryTests
{
    private const string Url = "http://localhost:9200";
    private const string IndexName = "test-index";

    private readonly ElasticDataRepository _repository;

    public ElasticDataRepositoryTests()
    {
        _repository = new ElasticDataRepository(Url, IndexName);
    }

    //TODO : Setup and mock Elastic data repo
    [Fact]
    public void InsertAll_ShouldInsertMultipleRecords()
    {
        // Arrange
        var records = new List<Transaction>
        {
            new Transaction
            {
                ID = 10,
                SourceAcount = 1001,
                DestiantionAccount = 2001,
                TransactionType = TransactionType.Paya,
                Amount = 100.0m,
                Date = DateTime.Now
            },
            new Transaction
            {
                ID = 20,
                SourceAcount = 1002,
                DestiantionAccount = 2002,
                TransactionType = TransactionType.Satna,
                Amount = 50.0m,
                Date = DateTime.Now.AddDays(-1)
            },
            new Transaction
            {
                ID = 30,
                SourceAcount = 1003,
                DestiantionAccount = 2003,
                TransactionType = TransactionType.KartBeKart,
                Amount = 200.0m,
                Date = DateTime.Now.AddDays(-2)
            }
        };

        // Act
        var response = _repository.InsertAll(records);

        // Assert
        response.Error.Should().BeFalse();
    }

    [Fact]
    public void Insert_ShouldInsertSingleRecord()
    {
        // Arrange
        var record = new Transaction
        {
            ID = 4,
            SourceAcount = 1001,
            DestiantionAccount = 2001,
            TransactionType = TransactionType.Paya,
            Amount = 100.0m,
            Date = DateTime.Now
        };

        // Act
        var response = _repository.Insert(record);

        // Assert
        response.Error.Should().BeFalse();
    }

    [Fact]
    public void Search_ShouldReturnDataGainResponse()
    {
        // Arrange
        ISearchRequest Selector(SearchDescriptor<Transaction> search) => search.MatchAll();

        // Act
        var response = _repository.Search((Func<SearchDescriptor<Transaction>, ISearchRequest>)Selector);

        // Assert
        response.Error.Should().BeFalse();
        response.Items.Should().NotBeNull();
    }

    //TODO : Implement Contain method
    [Fact]
    public void Contain_ShouldReturnTrueIfRecordExists()
    {
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenUrlIsNull()
    {
        // Arrange
        string url = null!;

        // Act
        Action act = () =>
        {
            var elasticDataRepository = new ElasticDataRepository(url, IndexName);
        };

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
    {
        // Arrange
        string name = null!;

        // Act
        var act = () =>
        {
            var elasticDataRepository = new ElasticDataRepository(Url, name);
        };

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void InsertAll_ShouldThrowArgumentNullException_WhenRecordsIsNull()
    {
        // Arrange
        List<Transaction> records = null!;

        // Act
        Action act = () => _repository.InsertAll(records);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Insert_ShouldThrowArgumentNullException_WhenRecordIsNull()
    {
        // Arrange
        Transaction record = null!;

        // Act
        Action act = () => _repository.Insert(record);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Search_ShouldThrowArgumentNullException_WhenSelectorIsNull()
    {
        // Arrange
        Func<SearchDescriptor<Transaction>, ISearchRequest> selector = null!;

        // Act
        Action act = () => _repository.Search(selector);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}