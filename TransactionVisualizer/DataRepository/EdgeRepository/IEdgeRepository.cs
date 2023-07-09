using Nest;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.DataRepository;

public interface IEdgeRepository
{
    void AddAll(List<Edge<Account, Transaction>> edges);

    List<Edge<Account, Transaction>> Search(
        Func<SearchDescriptor<Edge<Account, Transaction>>, ISearchRequest> selector);
}