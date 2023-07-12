using Nest;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public class SelectorBuilder : ISelectorBuilder
{
    public Func<SearchDescriptor<TVertex>, ISearchRequest> BuildKeyValueSelector<TVertex>(SelectorKeyValue keyValue)
        where TVertex : class
    {
        return descriptor => descriptor.Query(containerDescriptor =>
            containerDescriptor.Match(match =>
                match.Field(keyValue.Key).Query(keyValue.Value)
            )
        );
    }
}