using Nest;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public interface ISelectorBuilder
{
    public Func<SearchDescriptor<TVertex>, ISearchRequest> BuildKeyValueSelector<TVertex>(SelectorKeyValue keyValue)
        where TVertex : class;
}