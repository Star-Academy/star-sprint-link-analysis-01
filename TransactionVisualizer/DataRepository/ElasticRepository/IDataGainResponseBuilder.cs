using TransactionVisualizer.DataRepository.BaseDataRepository;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public interface IDataGainResponseBuilder<TResponse>
{
    DataGainResponse<TResponse> Build(bool errors, List<TResponse> items);
}