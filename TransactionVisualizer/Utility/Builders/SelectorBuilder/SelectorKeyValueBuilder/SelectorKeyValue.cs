namespace TransactionVisualizer.Utility.Graph;

public class SelectorKeyValue
{
    public string Key { get; set; }
    public string Value { get; set; }
    
    public SelectorKeyValue(string key, string value)
    {
        this.Key = key;
        this.Value = value;
    }

}