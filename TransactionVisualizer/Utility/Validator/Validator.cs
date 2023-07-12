using TransactionVisualizer.Exception;

namespace TransactionVisualizer.Utility.Validator;

public static class Validator
{
    public static void NullValidation(object? data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
    }

    public static void ListValidation<T>(List<T>? list)
    {
        ListValidation(list, nameof(list));
    }

    public static void ListValidation<T>(List<T>? list, string name)
    {
        NullValidation(list);

        if (list.Count == 0) throw new EmptyListException(name);
    }

     public static bool TypeValidator(Type target , Type destination)
     {
         return target != destination;
     }
}