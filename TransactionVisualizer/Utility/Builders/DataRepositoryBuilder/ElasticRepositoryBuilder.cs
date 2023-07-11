using TransactionVisualizer.DataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public class ElasticRepositoryBuilder : IElasticRepositoryBuilder
{
    private readonly IDataGainResponseBuilder<Account> _accountDataGainResponseBuilder;
    private readonly IDataGainResponseBuilder<Transaction> _transactionDataGainResponseBuilder;

    public static string ElasticUrl { get; } = "http://localhost:9200/";

    public ElasticRepositoryBuilder(IDataGainResponseBuilder<Account> accountDataGainResponseBuilder,
        IDataGainResponseBuilder<Transaction> transactionDataGainResponseBuilder)
    {
        _accountDataGainResponseBuilder = accountDataGainResponseBuilder;
        _transactionDataGainResponseBuilder = transactionDataGainResponseBuilder;
    }

    public IDataRepository<Account> BuildAccountRepository()
    {
        return new ElasticDataRepository<Account>(ElasticUrl, "accounts9",
            _accountDataGainResponseBuilder);
    }

    public IDataRepository<Transaction> BuildTransactionRepository()
    {
        return new ElasticDataRepository<Transaction>(ElasticUrl, "accounts9",
            _transactionDataGainResponseBuilder);
    }
}