using TransactionVisualizer.DataRepository.BaseDataRepository;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public abstract class DataManipulationResponseBuilder
{
    public static DataManipulationResponse Build(bool error)
    {
        return new DataManipulationResponse { Error = error };
    }
}