namespace TransactionVisualizer.Exception;
using System;
public class EmptyListException : Exception
{
    public EmptyListException(string? className) : base($"List of {className} is Empty.")
    {
    }
}