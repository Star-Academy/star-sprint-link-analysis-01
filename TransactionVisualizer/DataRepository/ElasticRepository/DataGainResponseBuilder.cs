using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public class DataGainResponseBuilder<TResponse> : IDataGainResponseBuilder<TResponse>
{
    public DataGainResponse<TResponse> Build(bool errors, List<TResponse> items)
    {
        Validator.NullValidationGroup(errors, items);
        
        return new DataGainResponse<TResponse>
        {
            Error = errors,
            Items = items
        };
    }
}