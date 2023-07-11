namespace TransactionVisualizer.Exception;

public class AccountNotFoundException : System.Exception
{
    public AccountNotFoundException(string? message) : base(message)
    {
    }
}