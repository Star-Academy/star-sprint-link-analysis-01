using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;

public class AccountRepository : IModelRepository<Account>
{
    private IDataRepository<Account> _dataRepository =
        new ElasticDataRepository<Account>("http://localhost:9200/", "accounts8");


    public void AddAll(List<Account> models)
    {
        _dataRepository.InsertAll(models);
    }

    public List<Account> Search(Func<SearchDescriptor<Account>, ISearchRequest> selector)
    {
        return _dataRepository.Search(selector).Items;
    }
}