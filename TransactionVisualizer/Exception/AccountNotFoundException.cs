namespace TransactionVisualizer.Exception;

using System;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string? message) : base(message)
    {
    }
}