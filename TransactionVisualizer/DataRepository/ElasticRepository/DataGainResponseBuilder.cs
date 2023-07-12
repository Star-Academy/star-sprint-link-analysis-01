using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

// Jalase di
public abstract class DataGainResponseBuilder<TResponse>
{
    public static DataGainResponse<TResponse> Build(bool errors, List<TResponse> items)
    {
        return new DataGainResponse<TResponse>
        {
            Error = errors,
            Items = items,
        };
    }
}