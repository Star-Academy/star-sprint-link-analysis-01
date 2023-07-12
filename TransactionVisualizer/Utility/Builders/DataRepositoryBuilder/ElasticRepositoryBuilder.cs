using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public class ElasticRepositoryBuilder : IElasticRepositoryBuilder
{
    private readonly IDataGainResponseBuilder<Account> _accountDataGainResponseBuilder;
    private readonly IDataGainResponseBuilder<Transaction> _transactionDataGainResponseBuilder;

    public ElasticRepositoryBuilder(IDataGainResponseBuilder<Account> accountDataGainResponseBuilder,
        IDataGainResponseBuilder<Transaction> transactionDataGainResponseBuilder)
    {
        _accountDataGainResponseBuilder = accountDataGainResponseBuilder;
        _transactionDataGainResponseBuilder = transactionDataGainResponseBuilder;
    }

    private static string ElasticUrl { get; } = "http://localhost:9200/";

    public IDataRepository<Account> BuildAccountRepository()
    {
        return new ElasticDataRepository<Account>(ElasticUrl, "accounts10",
            _accountDataGainResponseBuilder);
    }

    public IDataRepository<Transaction> BuildTransactionRepository()
    {
        return new ElasticDataRepository<Transaction>(ElasticUrl, "transaction8",
            _transactionDataGainResponseBuilder);
    }
}