using Nest;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public class ElasticDataRepository : IDataRepository
{
    public string Name { get; }
    private readonly ElasticClient _client;

    public ElasticDataRepository(string url, string name)
    {
        Validator.NullValidation(url);
        Validator.NullValidation(name);

        Name = name;
        _client = new ElasticClient(new ConnectionSettings(new Uri(url)).DefaultIndex(name));
    }


    public DataManipulationResponse InsertAll<TResponse>(List<TResponse> records) where TResponse : class
    {
        Validator.ListValidation(records);

        var bulkDescriptor = new BulkDescriptor();
        bulkDescriptor.CreateMany(records);

        var response = _client.Bulk(bulkDescriptor);

        return DataManipulationResponseBuilder.Build(response.Errors);
    }

    public DataManipulationResponse Insert<TResponse>(TResponse record) where TResponse : class
    {
        Validator.NullValidation(record);

        IIndexRequest<TResponse> request = new IndexDescriptor<TResponse>(record);

        var response = _client.Index(request);

        return DataManipulationResponseBuilder.Build(!response.IsValid);
    }


    //TODO : Generify search selector 
    public DataGainResponse<TResponse> Search<TResponse>(Func<SearchDescriptor<TResponse>, ISearchRequest> selector)
        where TResponse : class
    {
        Validator.NullValidation(selector);

        var response = _client.Search<TResponse>(selector.Invoke(new SearchDescriptor<TResponse>()));

        return DataGainResponseBuilder<TResponse>.Build(!response.IsValid, response.Documents.ToList());
    }

    //TODO : Implement this method 
    public bool Contain<TResponse, TSelector>(TSelector selector)
    {
        throw new NotImplementedException();
    }
    
    
}
