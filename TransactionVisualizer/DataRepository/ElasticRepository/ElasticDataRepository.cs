using Nest;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public class ElasticDataRepository<TResponse> : IDataRepository<TResponse> where TResponse : class
{
    private readonly ElasticClient _client;
    private readonly IDataGainResponseBuilder<TResponse> _dataGainResponseBuilder;

    public ElasticDataRepository(string url, string name, IDataGainResponseBuilder<TResponse> dataGainResponseBuilder)
    {
        Validator.NullValidationGroup(url, name);

        _dataGainResponseBuilder = dataGainResponseBuilder;
        _client = new ElasticClient(new ConnectionSettings(new Uri(url)).DefaultIndex(name));
    }

    public DataManipulationResponse InsertAll(List<TResponse> records)
    {
        Validator.ListValidation(records);

        var bulkDescriptor = new BulkDescriptor();
        bulkDescriptor.CreateMany(records);

        var response = _client.Bulk(bulkDescriptor);

        return DataManipulationResponseBuilder.Build(response.Errors);
    }

    public DataManipulationResponse Insert(TResponse record)
    {
        Validator.NullValidation(record);

        IIndexRequest<TResponse> request = new IndexDescriptor<TResponse>(record);

        var response = _client.Index(request);

        return DataManipulationResponseBuilder.Build(!response.IsValid);
    }


    public DataGainResponse<TResponse> Search(Func<SearchDescriptor<TResponse>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);

        var response = _client.Search<TResponse>(selector.Invoke(new SearchDescriptor<TResponse>()));

        return _dataGainResponseBuilder.Build(!response.IsValid, response.Documents.ToList());
    }
    
    public bool Contain(Func<SearchDescriptor<TResponse>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);
        
        return Search(selector).Items.Count > 0;
    }
}