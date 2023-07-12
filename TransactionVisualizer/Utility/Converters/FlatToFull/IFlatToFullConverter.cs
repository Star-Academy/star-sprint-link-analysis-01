namespace TransactionVisualizer.Utility.Converters.FlatToFull;

public interface IFlatToFullConverter<T, in TFlat> where T : class where TFlat : class
{
    public T Convert(TFlat flat);

    public List<T> ConvertAll(IEnumerable<TFlat> flats);
}