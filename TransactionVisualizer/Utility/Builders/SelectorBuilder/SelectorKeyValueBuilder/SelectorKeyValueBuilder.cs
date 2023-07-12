namespace TransactionVisualizer.Utility.Builders.SelectorBuilder;

using Validator;

public class SelectorKeyValueBuilder : ISelectorKeyValueBuilder
{
    public SelectorKeyValue BuildFindAccountById(string accountId)
    {
        Validator.NullValidation(accountId);
        
        return new SelectorKeyValue
        {
            Key = "id",
            Value = accountId
        };
    }

    public SelectorKeyValue BuildFindTransactionBySourceAccount(string sourceAccountId)
    {
        Validator.NullValidation(sourceAccountId);
        
        return new SelectorKeyValue
        {
            Key = "sourceAccount",
            Value = sourceAccountId
        };
    }
}