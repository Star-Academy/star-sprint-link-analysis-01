using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Services.Data;

public interface IDataService
{
    public bool AddAccounts(string filePath);
    public bool AddTransactions(string filePath);
}