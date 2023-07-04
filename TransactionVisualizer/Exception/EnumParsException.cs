namespace TransactionVisualizer.Exception;

public class EnumParsException : System.Exception
{
    public EnumParsException(string? message , string? enumName) : base($"{message} is not Proper Value for {enumName}")
    {
    }
}