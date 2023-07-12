using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizer.DataRepository.ElasticRepository;

public abstract class DataManipulationResponseBuilder
{
    public static DataManipulationResponse Build(bool error)
    {
        Validator.NullValidation(error);
        
        return new DataManipulationResponse { Error = error };
    }
}