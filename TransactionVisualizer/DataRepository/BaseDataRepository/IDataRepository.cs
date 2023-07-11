using Nest;

namespace TransactionVisualizer.DataRepository;

// TODO : Add TSelector to IDataRepository
// Jalase نوع داده جنریک برای کلاس تعریف شود
public interface IDataRepository<TResponse> where TResponse : class
{
    DataManipulationResponse InsertAll(List<TResponse> records);

    DataManipulationResponse Insert(TResponse record);

    DataGainResponse<TResponse> Search(Func<SearchDescriptor<TResponse>, ISearchRequest> selector);

    // TODO : Generify search selector
    // DataRepositoryResponse<TResponse> Search<TSelector>(TSelector selector);

    // TODO : Implement this method
    bool Contain<TSelector>(TSelector selector);
}