using Nest;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public class SelectorBuilder : ISelectorBuilder
{
    // public Func<SearchDescriptor<Account>, ISearchRequest> BuildAccountSelector(string accountId)
    // {
    //     return descriptor => descriptor.Query(containerDescriptor =>
    //         containerDescriptor.Match(match =>
    //             match.Field(f => f.Id).Query(accountId)
    //         )
    //     );
    // }
    //
    // public Func<SearchDescriptor<Transaction>, ISearchRequest> BuildTransactionSelectorBaseOnSourceAccountId(
    //     string sourceAccountId)
    // {
    //     return descriptor =>
    //         descriptor.Query(q =>
    //             q.Match(m =>
    //                 m.Field(f =>
    //                         f.SourceAccount)
    //                     .Query(sourceAccountId)
    //             )
    //         );
    // }

    public Func<SearchDescriptor<TVertex>, ISearchRequest> BuildKeyValueSelector<TVertex>(SelectorKeyValue keyValue) where TVertex : class
    {
        return descriptor => descriptor.Query(containerDescriptor =>
            containerDescriptor.Match(match =>
                match.Field(keyValue.Key).Query(keyValue.Value)
            )
        );
    }
}