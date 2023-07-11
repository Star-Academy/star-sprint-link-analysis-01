using TransactionVisualizer.DataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;

namespace TransactionVisualizer.Utility.Converters;

public class TransactionToEdge : IModelToGraphEdge<Transaction, Account, Transaction>
{
    private readonly IEdgeBuilder<Account, Transaction> _builder;
    private readonly IDataRepository<Account> _repository;
    private readonly ISelectorBuilder _selectorBuilder;

    public TransactionToEdge(IEdgeBuilder<Account, Transaction> builder, IDataRepository<Account> repository,
        ISelectorBuilder selectorBuilder)
    {
        _builder = builder;
        _repository = repository;
        _selectorBuilder = selectorBuilder;
    }

    public Edge<Account, Transaction> Convert(Transaction transaction)
    {
        var fromAccountSelector = _selectorBuilder.BuildAccountSelector(transaction.SourceAccount.ToString());
        var fromAccount = _repository.Search(fromAccountSelector).Items.First();

        var toAccountSelector = _selectorBuilder.BuildAccountSelector(transaction.DestinationAccount.ToString());
        var toAccount = _repository.Search(toAccountSelector).Items.First();

        return _builder.Build(new EdgeConfig<Account, Transaction>
            { Content = transaction, Destination = fromAccount, Source = toAccount, Weight = transaction.Amount });
    }
}