using Newtonsoft.Json;

namespace TransactionVisualizer.Utility.Parsers.FileParsers;

using Validator;

public class JsonFileParser<T> : IFileParser<T>
{
    public List<T>? Pars(StreamReader reader)
    {
        Validator.NullValidation(reader);
        
        var json = reader.ReadToEnd();

        // TODO: If type object of excepted different with actual type of json file return List
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}