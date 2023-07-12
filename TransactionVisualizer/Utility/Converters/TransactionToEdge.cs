using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;

namespace TransactionVisualizer.Utility.Converters;

using Validator;

public class TransactionToEdge : IModelToGraphEdge<Transaction, Account, Transaction>
{
    private readonly IEdgeBuilder<Account, Transaction> _builder;
    private readonly IDataRepository<Account> _repository;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;

    public TransactionToEdge(IEdgeBuilder<Account, Transaction> builder, IDataRepository<Account> repository,
        ISelectorBuilder selectorBuilder, ISelectorKeyValueBuilder selectorKeyValueBuilder)
    {
        Validator.NullValidationGroup
        (
            builder,
            repository,
            selectorBuilder,
            selectorKeyValueBuilder
        );

        _builder = builder;
        _repository = repository;
        _selectorBuilder = selectorBuilder;
        _selectorKeyValueBuilder = selectorKeyValueBuilder;
    }

    public Edge<Account, Transaction> Convert(Transaction transaction)
    {
        Validator.NullValidation(transaction);
        
        var fromAccountSelector =
            _selectorBuilder.BuildKeyValueSelector<Account>(
                _selectorKeyValueBuilder.BuildFindAccountById(transaction.SourceAccount.ToString()));

        var fromAccount = _repository.Search(fromAccountSelector).Items.First();

        var toAccountSelector =
            _selectorBuilder.BuildKeyValueSelector<Account>(
                _selectorKeyValueBuilder.BuildFindAccountById(transaction.DestinationAccount.ToString()));
        var toAccount = _repository.Search(toAccountSelector).Items.First();

        return _builder.Build(new EdgeConfig<Account, Transaction>
            { Content = transaction, Destination = toAccount, Source = fromAccount, Weight = transaction.Amount });
    }
}