using Newtonsoft.Json;

namespace TransactionVisualizer.Utility.Parsers.FileParser;

public class JsonFileParser<T> : IFileParser<T>
{
    public List<T>? Pars(string path)
    {
        var json = File.ReadAllText(path);

        // TODO: If type object of excepted different with actual type of json file return List
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}