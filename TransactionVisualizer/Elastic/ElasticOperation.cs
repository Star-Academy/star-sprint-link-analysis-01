using Nest;

namespace TransactionVisualizerTest;

public class ElasticOperation : IElasticOperation
{
    public string IndexName { get; set; }
    private ElasticClient Client { get; set; }

    public ElasticOperation(string url, string indexName)
    {
        IndexName = indexName;
        Client = new ElasticClient(new ConnectionSettings(new Uri(url)).DefaultIndex(indexName));
    }

    public BulkResponse IndexBulk<T>(List<T> documents) where T : class
    {
        return Client.IndexMany(documents);
    }

    public IndexResponse IndexDocument<T>(IIndexRequest<T> document) where T : class
    {
        return Client.Index<T>(document);
    }

    public ISearchResponse<T> SearchDocument<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector)
        where T : class
    {
        return Client.Search<T>(searchSelector.Invoke(new SearchDescriptor<T>()));
    }

    public bool Contain<T>(long id)
    {
        throw new NotImplementedException();
    }
}