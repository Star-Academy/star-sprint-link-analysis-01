using Nest;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.DataRepository.EdgeRepository;

public interface IEdgeRepository
{
    void AddAll(List<Edge<Account, Transaction>> edges);

    List<Edge<Account, Transaction>> Search(
        Func<SearchDescriptor<Edge<Account, Transaction>>, ISearchRequest> selector);
}