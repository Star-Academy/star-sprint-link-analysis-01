using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;

public class TransactionRepository : IDataRepository<Transaction>
{
    private readonly IDataRepository<Transaction> _dataRepository =
        new ElasticDataRepository<Transaction>("http://localhost:9200/", "transaction2");

    public DataManipulationResponse InsertAll(List<Transaction> records)
    {
        return _dataRepository.InsertAll(records);
    }

    public DataManipulationResponse Insert(Transaction record)
    {
        return _dataRepository.Insert(record);
    }

    public DataGainResponse<Transaction> Search(Func<SearchDescriptor<Transaction>, ISearchRequest> selector)
    {
        return _dataRepository.Search(selector);
    }

    public bool Contain<TSelector>(TSelector selector)
    {
        return _dataRepository.Contain(selector);
    }
}