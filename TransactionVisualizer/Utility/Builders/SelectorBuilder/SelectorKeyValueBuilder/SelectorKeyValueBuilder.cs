
namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

public class SelectorKeyValueBuilder : ISelectorKeyValueBuilder
{
    public SelectorKeyValue BuildFindAccountById(string accountId)
    {
        return new SelectorKeyValue
        {
            Key ="id", 
            Value = accountId
        };
    }

    public SelectorKeyValue BuildFindTransactionBySourceAccount(string sourceAccountId)
    {
        return new SelectorKeyValue
        {
            Key = "sourceAccount", 
            Value = sourceAccountId
        };
    }
}