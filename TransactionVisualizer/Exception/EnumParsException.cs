namespace TransactionVisualizer.Exception;
using System;
public class EnumParsException : Exception
{
    public EnumParsException(string? message , string? enumName) : base($"{message} is not Proper Value for {enumName}")
    {
    }
}