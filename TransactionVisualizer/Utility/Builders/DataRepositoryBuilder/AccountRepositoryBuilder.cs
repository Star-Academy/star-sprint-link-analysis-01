using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using static TransactionVisualizer.Utility.Constants.RepositoryConstants.ElasticConstants;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

using Validator;

public class AccountRepositoryBuilder : IElasticDataRepositoryBuilder<Account>
{
    private readonly IDataGainResponseBuilder<Account> _dataGainResponseBuilder;
    private const string IndexName = "account11";

    public AccountRepositoryBuilder(IDataGainResponseBuilder<Account> dataGainResponseBuilder)
    {
        Validator.NullValidation(dataGainResponseBuilder);
        
        _dataGainResponseBuilder = dataGainResponseBuilder;
    }

    public IDataRepository<Account> Build()
    {
        return new ElasticDataRepository<Account>(ElasticUrl, IndexName, _dataGainResponseBuilder);
    }
}