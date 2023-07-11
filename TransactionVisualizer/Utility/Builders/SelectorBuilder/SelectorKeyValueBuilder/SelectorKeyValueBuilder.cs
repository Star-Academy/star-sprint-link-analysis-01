using TransactionVisualizer.Utility.Graph;

namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public class SelectorKeyValueBuilder : ISelectorKeyValueBuilder
{
    public SelectorKeyValue BuildFindAccountById(string accountId)
    {
        return new SelectorKeyValue("id", accountId);
    }

    public SelectorKeyValue BuildFindTransactionBySourceAccount(string sourceAccountId)
    {
        return new SelectorKeyValue("sourceAccount", sourceAccountId);
    }
}