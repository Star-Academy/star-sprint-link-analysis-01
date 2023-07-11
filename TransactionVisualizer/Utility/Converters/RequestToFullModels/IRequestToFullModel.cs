namespace TransactionVisualizer.Utility.Converters.RequestToFullModels;

public interface IRequestToFullModel<in TRequest, out TModel>
{
    TModel Convert(TRequest request);
}