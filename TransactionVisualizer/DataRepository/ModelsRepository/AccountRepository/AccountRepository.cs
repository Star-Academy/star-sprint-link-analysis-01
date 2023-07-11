using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;

namespace TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;

public class AccountRepository : IDataRepository<Account>
{
    private readonly IDataRepository<Account> _dataRepository =
        new ElasticDataRepository<Account>("http://localhost:9200/", "accounts8");


    public DataManipulationResponse InsertAll(List<Account> records)
    {
        return _dataRepository.InsertAll(records);
    }

    public DataManipulationResponse Insert(Account record)
    {
        return _dataRepository.Insert(record);
    }

    DataGainResponse<Account> IDataRepository<Account>.Search(Func<SearchDescriptor<Account>, ISearchRequest> selector)
    {
        return _dataRepository.Search(selector);
    }

    public bool Contain<TSelector>(TSelector selector)
    {
        return _dataRepository.Contain(selector);
    }
}