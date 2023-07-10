using TransactionVisualizer.Models.BusinessModels;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ResponseModels;

namespace TransactionVisualizer.Utility.Converters.RequestToFullModels;

public interface IRequestToFullModel<in TRequest, out TModel>
{
    TModel Convert(TRequest request);
}