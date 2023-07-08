using System;
using System.Collections.Generic;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builder;
using TransactionVisualizer.Utility.ParserUtils;

namespace TransactionVisualizer.Utility;

public class GraphGenerator : IGraphGenerator<Account, Transaction>
{
    private CustomGraph<Account, Transaction> _graph { get; set; }

    private IEdgeBuilder<Account, Transaction> _edgeBuilder;

    //this field is just for test 
    private List<Account> Accounts { get; } = new List<Account?>();

    public void AddAccounts(List<Account> accounts)
    {
        Accounts.AddRange(accounts);
    }

    //test

    public GraphGenerator(IEdgeBuilder<Account, Transaction> edgeBuilder)
    {
        _edgeBuilder = edgeBuilder ?? throw new ArgumentNullException(nameof(edgeBuilder));
        _graph = new CustomGraph<Account?, Transaction>();
    }


    public CustomGraph<Account, Transaction> GenerateTransactionGraph(List<Transaction> transactions,
        Account account)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        if (transactions.Count == 0) throw new EmptyListException(nameof(Transaction));
        _graph.AddVertex(account);

        transactions.ForEach(transaction =>
            {
                var source = Accounts.Find(acc => acc.AccountId == transaction.SourceAccountId);
                var destination = Accounts.Find(acc => acc.AccountId == transaction.DestinationAccountId);

                Validation(source, destination);

                _graph.AddEdge(_edgeBuilder.Build(new EdgeConfig<Account, Transaction>()
                    { Source = source, Destination = destination, Content = transaction }));
            }
        );
        return _graph;
    }

    private static void Validation(Account? source, Account? destination)
    {
        if (source == null)
        {
            throw new AccountNotFoundException("Source Account Not Found");
        }

        ;
        if (destination == null)
        {
            throw new AccountNotFoundException("Destination Account Not Found");
        }

        ;
    }

    public CustomGraph<Account, Transaction> Expand(Account? account, List<Transaction> transactions)
    {
        return GenerateTransactionGraph(transactions, account);
    }
}