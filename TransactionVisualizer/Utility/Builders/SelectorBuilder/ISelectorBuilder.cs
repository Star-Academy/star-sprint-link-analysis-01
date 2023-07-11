using Nest;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public interface ISelectorBuilder
{
    public Func<SearchDescriptor<Account>, ISearchRequest> BuildAccountSelector(string accountId);

    public Func<SearchDescriptor<Transaction>, ISearchRequest> BuildTransactionSelectorBaseOnSourceAccountId(
        string sourceAccountId);
}