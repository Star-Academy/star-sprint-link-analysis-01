using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;
using static TransactionVisualizer.Utility.Constants.RepositoryConstants.ElasticConstants;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public class TransactionRepositoryBuilder : IElasticDataRepositoryBuilder<Transaction>
{
    private readonly IDataGainResponseBuilder<Transaction> _dataGainResponseBuilder;
    private const string IndexName = "transaction10";

    public TransactionRepositoryBuilder(IDataGainResponseBuilder<Transaction> dataGainResponseBuilder)
    {
        _dataGainResponseBuilder = dataGainResponseBuilder;
    }

    public IDataRepository<Transaction> Build()
    {
        return new ElasticDataRepository<Transaction>(ElasticUrl, IndexName, _dataGainResponseBuilder);
    }
}