using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public interface ISelectorKeyValueBuilder
{
    public SelectorKeyValue BuildFindAccountById(string accountId);
    public SelectorKeyValue BuildFindTransactionBySourceAccount(string sourceAccountId);
}