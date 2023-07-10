using Nest;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;

namespace TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;

public class TransactionRepository : IModelRepository<Transaction>
{
    private IDataRepository<Transaction> _dataRepository =
        new ElasticDataRepository<Transaction>("http://localhost:9200/", "transaction");

    public void AddAll(List<Transaction> models)
    {
        _dataRepository.InsertAll(models);
    }

    public List<Transaction> Search(Func<SearchDescriptor<Transaction>, ISearchRequest> selector)
    {
        return _dataRepository.Search(selector).Items;
    }
}