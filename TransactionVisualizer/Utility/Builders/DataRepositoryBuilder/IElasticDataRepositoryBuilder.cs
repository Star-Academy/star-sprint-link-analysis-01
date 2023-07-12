using TransactionVisualizer.DataRepository.BaseDataRepository;

namespace TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;

public interface IElasticDataRepositoryBuilder<T> where T : class
{
    public IDataRepository<T> Build();
    
}