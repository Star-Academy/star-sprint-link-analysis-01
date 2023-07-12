using Nest;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.DataRepository;

public interface IEdgeRepository
{
    void AddAll(List<Edge<Account, Transaction>> edges);

    List<Edge<Account, Transaction>> Search(
        Func<SearchDescriptor<Edge<Account, Transaction>>, ISearchRequest> selector);
}