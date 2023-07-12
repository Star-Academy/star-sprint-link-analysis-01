using Newtonsoft.Json;

namespace TransactionVisualizer.Utility.Parsers.FileParsers;

public class JsonFileParser<T> : IFileParser<T>
{
    public List<T>? Pars(StreamReader reader)
    {
        var json = reader.ReadToEnd();

        // TODO: If type object of excepted different with actual type of json file return List
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}