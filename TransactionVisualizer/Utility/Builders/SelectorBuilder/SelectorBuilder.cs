using Nest;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

using Validator;

public class SelectorBuilder : ISelectorBuilder
{
    public Func<SearchDescriptor<TVertex>, ISearchRequest> BuildKeyValueSelector<TVertex>(SelectorKeyValue keyValue)
        where TVertex : class
    {
        Validator.NullValidation(keyValue);
        
        return descriptor => descriptor.Query(containerDescriptor =>
            containerDescriptor.Match(match =>
                match.Field(keyValue.Key).Query(keyValue.Value)
            )
        );
    }
}