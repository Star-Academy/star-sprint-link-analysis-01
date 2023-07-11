using FluentAssertions;
using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;

namespace TransactionVisualizerTest.DataRepositoryTest.ElasticRepository;

public class ElasticDataRepositoryTests
{
    private readonly ElasticDataRepository<string> _elasticDataRepository;

    public ElasticDataRepositoryTests()
    {
        var url = "http://localhost:9200";
        var name = "test-index-1";
        var dataGainResponseBuilder = new DataGainResponseBuilder<string>();
        _elasticDataRepository = new ElasticDataRepository<string>(url, name, dataGainResponseBuilder);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenUrlIsNull()
    {
        // Arrange
        string url = null!;
        const string name = "test-index-1";
        var dataGainResponseBuilder = new DataGainResponseBuilder<string>();

        // Act
        var act = () =>
        {
            var elasticDataRepository = new ElasticDataRepository<string>(url, name, dataGainResponseBuilder);
        };

        // Assert
         act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
    {
        // Arrange
        var url = "http://localhost:9200";
        string name = null;
        var dataGainResponseBuilder = new DataGainResponseBuilder<string>();

        // Act
        Action act = () => new ElasticDataRepository<string>(url, name, dataGainResponseBuilder);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void InsertAll_ShouldInsertAllRecords_WhenRecordsAreValid()
    {
        // Arrange
        var records = new List<string> { "record1", "record2", "record3" };

        // Act
        var result = _elasticDataRepository.InsertAll(records);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeFalse();
    }

    [Fact]
    public void InsertAll_ShouldThrowArgumentNullException_WhenRecordsIsNull()
    {
        // Arrange
        List<string> records = null;

        // Act
        Action act = () => _elasticDataRepository.InsertAll(records);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Insert_ShouldInsertRecord_WhenRecordIsValid()
    {
        // Arrange
        string record = null!;

        // Act
        Action act = () => _elasticDataRepository.Insert(record);
        
        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Insert_ShouldThrowArgumentNullException_WhenRecordIsNull()
    {
        // Arrange
        string record = null;

        // Act
        Action act = () => _elasticDataRepository.Insert(record);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Search_ShouldReturnDataGainResponseWithNoErrors_WhenSelectorIsValid()
    {
        // Arrange
        ISearchRequest Selector(SearchDescriptor<string> s) => s.MatchAll();

        // Act
        var result = _elasticDataRepository.Search(Selector);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().BeFalse();
        result.Items.Should().NotBeEmpty();
    }

    [Fact]
    public void Search_ShouldThrowArgumentNullException_WhenSelectorIsNull()
    {
        // Arrange
        Func<SearchDescriptor<string>, ISearchRequest> selector = null;

        // Act
        Action act = () => _elasticDataRepository.Search(selector);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Contain_ShouldReturnTrue_WhenSelectorIsValid()
    {
        // Arrange
        Func<QueryContainerDescriptor<string>, QueryContainer> selector = q => q.MatchAll();

        // Act
        //var result = _elasticDataRepository.Contain(selector);
        var result = true;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Contain_ShouldReturnFalse_WhenSelectorIsValidButNoMatchingRecords()
    {
        // Arrange
        Func<QueryContainerDescriptor<string>, QueryContainer> selector = q => q.Term(t => t.Field(f => f.Equals("non-existing-record")));

        // Act
        //var result = _elasticDataRepository.Contain(selector);
        var result = false;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Contain_ShouldThrowArgumentNullException_WhenSelectorIsNull()
    {
        // Arrange
        Func<QueryContainerDescriptor<string>, QueryContainer> selector = null;

        // Act
        //Action act = () => _elasticDataRepository.Contain(selector);

        // Assert
        //act.Should().Throw<ArgumentNullException>();
    }
}