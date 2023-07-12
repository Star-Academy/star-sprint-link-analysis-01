using TransactionVisualizer.DataRepository.BaseDataRepository;

namespace TransactionVisualizer.Services.Data;

public interface IDataService
{
    public DataManipulationResponse AddAccounts(string filePath);
    public DataManipulationResponse AddTransactions(string filePath);
}