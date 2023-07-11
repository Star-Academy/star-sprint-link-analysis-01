using TransactionVisualizer.DataRepository.BaseDataRepository;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

// OwnerID di
public class DataGainResponseBuilder<TResponse> : IDataGainResponseBuilder<TResponse>
{
    public DataGainResponse<TResponse> Build(bool errors, List<TResponse> items)
    {
        return new DataGainResponse<TResponse>
        {
            Error = errors,
            Items = items
        };
    }
}