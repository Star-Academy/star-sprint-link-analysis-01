using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility;

public interface IGraphGenerator<T,U>
{
    void GenerateTransactionGraph(List<Transaction> transactions,
        Account? account);

    void Expand(Account? account, List<Transaction> transactions);
    CustomGraph<T , U> GetGraph();
}