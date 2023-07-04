using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility;

public class GraphGenerator : IGraphGenerator<Account, Transaction>
{
    private CustomGraph<Account?, Transaction> _graph = new CustomGraph<Account?, Transaction>();

    //this field is just for test 
    private  List<Account?> _accounts = new List<Account?>();

    public void AddAccounts(List<Account> accounts)
    {
        _accounts.AddRange(accounts);
    }

    public List<Account> GetAccounts()
    {
        return _accounts;
    }
    //test


    public void GenerateTransactionGraph(List<Transaction> transactions, Account? account)
    {
        if (account == null | transactions.Count < 1) throw new ArgumentNullException();

        _graph.AddVertex(account);

        transactions.ForEach(transaction =>
            {
                Account? source = _accounts.Find(acc => acc.AccountID == transaction.SourceAcount);
                Account? destination = _accounts.Find(acc => acc.AccountID == transaction.DestiantionAccount);
            
            
                if (source.AccountID != account.AccountID) _graph.AddVertex(source);
                else _graph.AddVertex(destination);
            
            
                if (source == null || destination == null) throw new AccountNotFoundException();
            
                _graph.AddEdge(transaction, source, destination);
            }
        );
    }
    public void Expand(Account? account, List<Transaction> transactions)
    {
        GenerateTransactionGraph(transactions, account);
    }

    public CustomGraph<Account, Transaction> GetGraph()
    {
        return _graph;
    }
}