namespace TransactionVisualizer;

public class AccountNotFoundException : System.Exception
{
    public AccountNotFoundException(string? message) : base(message)
    {
    }
}