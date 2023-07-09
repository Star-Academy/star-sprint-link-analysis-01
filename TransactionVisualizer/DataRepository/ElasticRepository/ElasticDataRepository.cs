using Nest;
using TransactionVisualizer.Utility.Validator;
using System.Diagnostics.Contracts;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public class ElasticDataRepository<TResponse> : IDataRepository<TResponse> where TResponse : class
{
    public string Name { get; }
    private readonly ElasticClient _client;

    // Jalase درست کردن کلاس برای ورودی کانستراکتور و اجرای فلونت ولیدیشن روی ان 
    public ElasticDataRepository(string url, string name)
    {
        Validator.NullValidation(url);
        Validator.NullValidation(name);

        Name = name;
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

    //TODO : Generify search selector 
    public DataGainResponse<TResponse> Search(Func<SearchDescriptor<TResponse>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);

        var response = _client.Search<TResponse>(selector.Invoke(new SearchDescriptor<TResponse>()));

        return DataGainResponseBuilder<TResponse>.Build(!response.IsValid, response.Documents.ToList());
    }

    //TODO : Implement this method 
    public bool Contain<TSelector>(TSelector selector)
    {
        throw new NotImplementedException();
    }
}