using Nest;

namespace TransactionVisualizer.DataRepository.BaseDataRepository;

public interface IDataRepository<TResponse> where TResponse : class
{
    DataManipulationResponse InsertAll(List<TResponse> records);

    DataManipulationResponse Insert(TResponse record);

    DataGainResponse<TResponse> Search(Func<SearchDescriptor<TResponse>, ISearchRequest> selector);

    // TODO : Generify search selector
    // DataRepositoryResponse<TResponse> Search<TSelector>(TSelector selector);

    // TODO : Implement this method
    bool Contain(Func<SearchDescriptor<TResponse>, ISearchRequest> selector);
}