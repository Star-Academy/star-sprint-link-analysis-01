using Nest;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public interface ISelectorBuilder
{
    public Func<SearchDescriptor<TVertex>, ISearchRequest> BuildKeyValueSelector<TVertex>(SelectorKeyValue keyValue)
        where TVertex : class;
}