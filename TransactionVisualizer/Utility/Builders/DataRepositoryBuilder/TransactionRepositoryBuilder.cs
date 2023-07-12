using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;
using static TransactionVisualizer.Utility.Constants.RepositoryConstants.ElasticConstants;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using Validator;


public class TransactionRepositoryBuilder : IElasticDataRepositoryBuilder<Transaction>
{
    private readonly IDataGainResponseBuilder<Transaction> _dataGainResponseBuilder;
    private const string IndexName = "transaction11";

    public TransactionRepositoryBuilder(IDataGainResponseBuilder<Transaction> dataGainResponseBuilder)
    {
        Validator.NullValidation(dataGainResponseBuilder);
        
        _dataGainResponseBuilder = dataGainResponseBuilder;
    }

    public IDataRepository<Transaction> Build()
    {
        return new ElasticDataRepository<Transaction>(ElasticUrl, IndexName, _dataGainResponseBuilder);
    }
}