using Nest;

namespace TransactionVisualizer.Elastic;

public interface IElasticOperation
{
    BulkResponse IndexBulk<T>(List<T> documents) where T : class;
    IndexResponse IndexDocument<T>(IIndexRequest<T> document) where T : class;
    ISearchResponse<T> SearchDocument<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class;
    bool Contain<T>(long id);
}