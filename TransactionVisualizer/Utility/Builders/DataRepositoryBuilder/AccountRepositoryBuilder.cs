using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using static TransactionVisualizer.Utility.Constants.RepositoryConstants.ElasticConstants;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public class AccountRepositoryBuilder : IElasticDataRepositoryBuilder<Account>
{
    private readonly IDataGainResponseBuilder<Account> _dataGainResponseBuilder;
    private const string IndexName = "account10";

    public AccountRepositoryBuilder(IDataGainResponseBuilder<Account> dataGainResponseBuilder)
    {
        _dataGainResponseBuilder = dataGainResponseBuilder;
        
    }

    public IDataRepository<Account> Build()
    {
        return new ElasticDataRepository<Account>(ElasticUrl, IndexName, _dataGainResponseBuilder);
    }
}