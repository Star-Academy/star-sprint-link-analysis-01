using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility;

public interface IGraphGenerator<T,U>
{
    CustomGraph<Account, Transaction> GenerateTransactionGraph(List<Transaction> transactions,
        Account account);

    CustomGraph<Account, Transaction> Expand(Account account, List<Transaction> transactions);
    
}