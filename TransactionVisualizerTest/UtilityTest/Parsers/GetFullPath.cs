namespace TransactionVisualizerTest.UtilityTest.Parsers;

public class GetFullPath
{
    public static string ConvertRelativeToAbsolute(string relative)
    {
        var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        var parentFullName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        
        return parentFullName.Remove(parentFullName.Length - 4) + relative;
    }
}