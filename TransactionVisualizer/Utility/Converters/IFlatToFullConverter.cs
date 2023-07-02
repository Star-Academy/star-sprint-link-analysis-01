namespace TransactionVisualizer.Utility.Converters;

public interface IFlatToFullConverter<T , U>
{
    List<T> ConvertAll(List<U> flatList);
    T Convert(U flat);
}