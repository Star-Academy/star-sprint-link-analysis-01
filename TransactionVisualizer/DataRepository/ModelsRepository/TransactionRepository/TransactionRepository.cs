using Nest;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.DataRepository.ElasticRepository;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

namespace TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;

public class TransactionRepository : IDataRepository<Transaction>
{
    private readonly IDataRepository<Transaction> _dataRepository;
    private readonly IElasticRepositoryBuilder _elasticRepositoryBuilder;

    public TransactionRepository(IElasticRepositoryBuilder elasticRepositoryBuilder)
    {
        _elasticRepositoryBuilder = elasticRepositoryBuilder;
        _dataRepository = elasticRepositoryBuilder.BuildTransactionRepository();
    }


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