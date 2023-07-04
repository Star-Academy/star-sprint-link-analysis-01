using TransactionVisualizer.Exception;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility;

public class GraphGenerator : IGraphGenerator<Account, Transaction>
{
    private CustomGraph<Account?, Transaction> _graph { get; set; }= new CustomGraph<Account?, Transaction>();

    //this field is just for test 
    private List<Account> Accounts { get; } = new List<Account?>();

    public void AddAccounts(List<Account> accounts)
    {
        Accounts.AddRange(accounts);
    }

    //test


    public CustomGraph<Account , Transaction> GenerateTransactionGraph(List<Transaction> transactions,
        Account account)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        if (transactions.Count == 0) throw new EmptyListException(nameof(Transaction));
        _graph.AddVertex(account);

        transactions.ForEach(transaction =>
            {
                Account? source = Accounts.Find(acc => acc.AccountID == transaction.SourceAcount);
                Account? destination = Accounts.Find(acc => acc.AccountID == transaction.DestiantionAccount);

                if (source == null) throw new AccountNotFoundException("Source Account Not Found");
                if (destination == null) throw new AccountNotFoundException("Destination Account Not Found");

                _graph.AddEdge(transaction, source, destination);
            }
        );
        return _graph;
    }

    public CustomGraph<Account , Transaction> Expand(Account? account, List<Transaction> transactions)
    {
        return GenerateTransactionGraph(transactions, account);
    }

}