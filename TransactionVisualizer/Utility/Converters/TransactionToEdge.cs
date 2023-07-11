using TransactionVisualizer.DataRepository.ModelsRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;

namespace TransactionVisualizer.Utility.Converters;

public class TransactionToEdge : IModelToGraphEdge<Transaction, Account, Transaction>
{
    private IEdgeBuilder<Account, Transaction> _builder;
    private IModelRepository<Account> _repository;

    public TransactionToEdge(IEdgeBuilder<Account, Transaction> builder, IModelRepository<Account> repository)
    {
        _builder = builder;
        _repository = repository;
    }

    public Edge<Account, Transaction> Convert(Transaction transaction)
    {
        var fromAccount = _repository
            .Search(descriptor => descriptor.Query(containerDescriptor =>
                containerDescriptor.Match(match =>
                    match.Field(f => f.Id).Query(transaction.SourceAccount.ToString())))).First();
        
        
        
        var toAccount = _repository
            .Search(descriptor => descriptor.Query(containerDescriptor =>
                containerDescriptor.Match(match =>
                    match.Field(f => f.Id).Query(transaction.DestinationAccount.ToString())))).First();
        
        
        return _builder.Build(new EdgeConfig<Account, Transaction>()
            { Content = transaction, Destination = fromAccount, Source = toAccount, Weight = transaction.Amount });
    }
}