using System.Collections.Generic;

namespace TransactionVisualizer.Utility.Converters;

public interface IFlatToFullConverter<T , U> where T : class where U : class
{
    List<T> ConvertAll(List<U> flatList);
    T Convert(U flat);
}