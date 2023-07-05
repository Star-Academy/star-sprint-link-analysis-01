using System.Collections.Generic;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public abstract class DataGainResponseBuilder<TResponse>
{
    public static DataGainResponse<TResponse> Build(bool errors, List<TResponse>? items)
    {
        return new DataGainResponse<TResponse>()
        {
            Error = errors,
            Items = items,
        };
    }
}