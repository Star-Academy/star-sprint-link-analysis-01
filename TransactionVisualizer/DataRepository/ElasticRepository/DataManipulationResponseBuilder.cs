namespace TransactionVisualizer.DataRepository.ElasticRepository;

// Jalase di
public abstract class DataManipulationResponseBuilder
{
    public static DataManipulationResponse Build(bool error)
    {
        return new DataManipulationResponse { Error = error };
    }
}