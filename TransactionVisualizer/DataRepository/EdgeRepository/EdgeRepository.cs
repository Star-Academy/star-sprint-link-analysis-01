using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.DataRepository.EdgeRepository;

public class EdgeRepository : IEdgeRepository
{
    private IDataRepository<Edge<Account, Transaction>> _dataRepository =
        new ElasticDataRepository<Edge<Account, Transaction>>("http://localhost:9200/", "edges3");

    public void AddAll(List<Edge<Account, Transaction>> edges)
    {
        _dataRepository.InsertAll(edges);
    }

    public List<Edge<Account, Transaction>> Search(
        Func<SearchDescriptor<Edge<Account, Transaction>>, ISearchRequest> selector)
    {
        var response = _dataRepository.Search(selector);
        return response.Items!;
    }
}