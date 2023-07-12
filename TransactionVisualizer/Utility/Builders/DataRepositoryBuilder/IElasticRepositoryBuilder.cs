using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public interface IElasticRepositoryBuilder
{
    public IDataRepository<Account> BuildAccountRepository();
    public IDataRepository<Transaction> BuildTransactionRepository();
}