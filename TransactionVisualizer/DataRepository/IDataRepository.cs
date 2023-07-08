using Nest;

namespace TransactionVisualizer.DataRepository;

public interface IDataRepository
{
    DataManipulationResponse InsertAll<TResponse>(List<TResponse> records) where TResponse : class;

    DataManipulationResponse Insert<TResponse>(TResponse record) where TResponse : class;

    DataGainResponse<TResponse> Search<TResponse>(Func<SearchDescriptor<TResponse>, ISearchRequest> selector)
        where TResponse : class;

    //TODO : Generify search selector
    // DataRepositoryResponse<TResponse> Search<TResponse, TSelector>(TSelector selector) where TResponse : class;
    bool Contain<TResponse, TSelector>(TSelector selector);
}