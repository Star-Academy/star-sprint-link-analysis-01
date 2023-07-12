using Nest;
using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ModelsRepository.TransactionRepository;

public class TransactionRepository : IDataRepository<Transaction>
{
    private readonly IDataRepository<Transaction> _dataRepository;

    public TransactionRepository(IElasticRepositoryBuilder elasticRepositoryBuilder)
    {
        Validator.NullValidation(elasticRepositoryBuilder);
        
        _dataRepository = elasticRepositoryBuilder.BuildTransactionRepository();
    }

    public DataManipulationResponse InsertAll(List<Transaction> records)
    {
        Validator.ListValidation(records);
        
        return _dataRepository.InsertAll(records);
    }

    public DataManipulationResponse Insert(Transaction record)
    {
        Validator.NullValidation(record);
        
        return _dataRepository.Insert(record);
    }

    public DataGainResponse<Transaction> Search(Func<SearchDescriptor<Transaction>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);
        
        return _dataRepository.Search(selector);
    }

    public bool Contain(Func<SearchDescriptor<Transaction>, ISearchRequest> selector)
    {
        Validator.NullValidation(selector);
        
        return _dataRepository.Contain(selector);
    }
}