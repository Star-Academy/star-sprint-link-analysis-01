namespace TransactionVisualizer.Exception;

public class EmptyListException : System.Exception
{
    public EmptyListException(string? className) : base($"List of {className} is Empty.")
    {
    }
}