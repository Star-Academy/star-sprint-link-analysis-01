using Nest;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ModelsRepository.AccountRepository;

public class AccountRepository : IDataRepository<Account>
{
    private readonly IDataRepository<Account> _dataRepository;


    public AccountRepository(IElasticRepositoryBuilder elasticRepositoryBuilder)
    {
        Validator.NullValidation(elasticRepositoryBuilder);
        
        _dataRepository = elasticRepositoryBuilder.BuildAccountRepository();
    }

    public DataManipulationResponse InsertAll(List<Account> records)
    {
        Validator.ListValidation(records);
        
        return _dataRepository.InsertAll(records);
    }

    public DataManipulationResponse Insert(Account record)
    {
        Validator.NullValidation(record);
        
        return _dataRepository.Insert(record);
    }

    public DataGainResponse<Account> Search(Func<SearchDescriptor<Account>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);
        
        return _dataRepository.Search(selector);
    }

    public bool Contain(Func<SearchDescriptor<Account>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);
        
        return _dataRepository.Contain(selector);
    }
}