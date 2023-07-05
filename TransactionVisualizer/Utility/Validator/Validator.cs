using TransactionVisualizer.Exception;

namespace TransactionVisualizer.Utility.Validator;

public abstract class Validator
{
    public static void NullValidation(object data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }
    }

    public static void ListValidation<T>(List<T> list)
    {
        
        ListValidation(list, nameof(list));
    }

    public static void ListValidation<T>(List<T> list, string name)
    {
        NullValidation(list);
        
        if (list.Count == 0)
        {
            throw new EmptyListException(name);
        }
    }
}